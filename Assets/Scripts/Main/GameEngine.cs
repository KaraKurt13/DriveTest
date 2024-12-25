using Assets.Scripts.Car;
using Assets.Scripts.Helpers;
using Assets.Scripts.SaveData;
using Assets.Scripts.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using ExitGames.Client.Photon;

namespace Assets.Scripts.Main
{
    public class GameEngine : MonoBehaviour
    {
        public bool IsGameRunning { get; private set; } = false;

        public static bool IsMultiplayerGame => PhotonNetwork.IsConnected && PhotonNetwork.InRoom;

        public int TicksTillEnd { get; private set; }

        public GameObject PlayerCar;

        public CameraController CameraController;

        public GameComponentsController ComponentsController;

        public SceneController SceneController;

        [SerializeField]
        private Transform _streetSpawnPoint, _garageSpawnPoint;

        private void FixedUpdate()
        {
            if (IsGameRunning)
                GameTick();
        }

        private void Update()
        {
            if (IsWaitingForPlayers)
            {
                if (AllPlayersReady())
                {
                    ComponentsController.WaitingScreen.Hide();
                    StartGame();
                }
            }

        }

        private void GameTick()
        {
            TicksTillEnd--;
            if (TicksTillEnd <= 0)
                EndGame();
        }

        public void SwitchToGarage()
        {
            PlayerCar.transform.position = _garageSpawnPoint.position;
            CameraController.SwitchToGarageCamera();
            ComponentsController.GarageComponent.Draw();
            ComponentsController.PlayerStatsComponent.Hide();
        }

        public void SwitchToStreet()
        {
            PlayerCar.transform.position = GetSpawnPoint();
            CameraController.SwitchToCarCamera();
            ComponentsController.GarageComponent.Hide();
            ComponentsController.PlayerStatsComponent.Draw();
        }

        public void InitializePlayer(GameObject playerCar)
        {
            PlayerCar = playerCar;
            var playerStatsTracker = PlayerCar.GetComponent<StatsTracker>();
            var playerCamera = PlayerCar.GetComponent<CarVisualizer>().CarCamera;
            CameraController.SetCarCamera(playerCamera);
            ComponentsController.PlayerStatsComponent.Init(playerStatsTracker);
            StartGame();
        }

        public void StartGame()
        {
            PlayerCar.GetComponent<CarController>().IsControllable = true;
            TicksTillEnd = TimeHelper.SecondsToTicks(10f);
            IsGameRunning = true;
            IsWaitingForPlayers = false;
        }

        public void EndGame()
        {
            PlayerCar.GetComponent<CarController>().IsControllable = false; // temp
            IsGameRunning = false;
            var score = PlayerCar.GetComponent<StatsTracker>().Score; // temp
            var earnedCash = Mathf.CeilToInt(score * Constants.ScoreToMoneyMultiplayer);
            ComponentsController.GameResultsSubcomponent.Draw(score, earnedCash);
            SaveSystem.PlayerData.Cash += earnedCash;
            SaveSystem.SavePlayerData();
        }

        public void ReturnToMenu()
        {
            SceneController.LoadMainMenu();
        }

        public Vector3 GetSpawnPoint()
        {
            var position = _streetSpawnPoint.position;

            if (IsMultiplayerGame)
                position.z = PhotonNetwork.LocalPlayer.ActorNumber * 5f;

            return position;
        }

        #region Online
        public bool IsWaitingForPlayers = true;

        public void InitializeOnlinePlayer()
        {
            var playerSettings = SaveSystem.PlayerData.CarData;
            var carName = DataLibrary.Instance.CarsData[playerSettings.Car].Name;
            PlayerCar = PhotonNetwork.Instantiate($"Prefabs/Cars/OnlineCars/{carName}", GetSpawnPoint(), Quaternion.identity);
            var playerStatsTracker = PlayerCar.GetComponent<StatsTracker>();
            var carVisualizer = PlayerCar.GetComponent<CarVisualizer>();
            var photonView = PlayerCar.GetComponent<PhotonView>();

            photonView.RPC("ApplyPaintPhoton", RpcTarget.All, playerSettings.Paint);
            CameraController.SetCarCamera(carVisualizer.CarCamera);
            ComponentsController.PlayerStatsComponent.Init(playerStatsTracker);
            SetPlayerReady();
            ComponentsController.WaitingScreen.Draw();
        }

        private void SetPlayerReady()
        {
            Hashtable props = new Hashtable
            {
                { "IsReady", true }
            };
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        }

        public static bool AllPlayersReady()
        {
            if (GetReadyPlayersAmount() == PhotonNetwork.CurrentRoom.PlayerCount)
                return true;
            else
                return false;
        }

        public static int GetReadyPlayersAmount()
        {
            int readyCount = 0;

            foreach (var player in PhotonNetwork.PlayerList)
            {
                if (player.CustomProperties.TryGetValue("IsReady", out var isReady) && (bool)isReady)
                {
                    readyCount++;
                }
            }
            Debug.Log(readyCount);
            return readyCount;
        }
        #endregion Online
    }
}
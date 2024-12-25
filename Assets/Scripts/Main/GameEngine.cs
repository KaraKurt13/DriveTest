using Assets.Scripts.Car;
using Assets.Scripts.Helpers;
using Assets.Scripts.SaveData;
using Assets.Scripts.UI;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

namespace Assets.Scripts.Main
{
    public class GameEngine : MonoBehaviour
    {
        public bool IsGameRunning { get; private set; } = false;

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
            PlayerCar.transform.position = _streetSpawnPoint.position;
            CameraController.SwitchToCarCamera();
            ComponentsController.GarageComponent.Hide();
            ComponentsController.PlayerStatsComponent.Draw();
        }

        public void InitializePlayerCar(GameObject playerCar)
        {
            PlayerCar = playerCar;
            var playerStatsTracker = PlayerCar.GetComponent<StatsTracker>();
            var playerCamera = PlayerCar.GetComponent<CarVisualizer>().CarCamera;
            PlayerCar.GetComponent<CarController>().IsControllable = true;
            CameraController.SetCarCamera(playerCamera);
            ComponentsController.PlayerStatsComponent.Init(playerStatsTracker);
        }

        public void StartGame()
        {
            TicksTillEnd = TimeHelper.SecondsToTicks(10f);
            IsGameRunning = true;
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
    }
}
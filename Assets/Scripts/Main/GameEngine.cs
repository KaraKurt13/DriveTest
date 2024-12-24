using Assets.Scripts.Car;
using Assets.Scripts.Helpers;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Main
{
    public class GameEngine : MonoBehaviour
    {
        public bool IsGameRunning { get; private set; } = false;

        public int TicksTillEnd { get; private set; }

        public GameObject PlayerCar;

        public CameraController CameraController;

        public GameComponentsController ComponentsController;

        [SerializeField]
        private Transform _streetSpawnPoint, _garageSpawnPoint;

        private void Start()
        {
            StartGame();
        }

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

        public void StartGame()
        {
            TicksTillEnd = TimeHelper.SecondsToTicks(10f);
            PlayerCar.GetComponent<CarController>().IsControllable = true; // temp
            IsGameRunning = true;
        }

        public void EndGame()
        {
            PlayerCar.GetComponent<CarController>().IsControllable = false; // temp
            IsGameRunning = false;
            ComponentsController.GameResultsSubcomponent.Draw(PlayerCar.GetComponent<StatsTracker>().Score); // temp
        }

        public void ReturnToMenu()
        {
            SceneController.Instance.LoadMainMenu();
        }
    }
}
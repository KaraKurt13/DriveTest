using Assets.Scripts.Car;
using Assets.Scripts.Helpers;
using Assets.Scripts.SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GarageComponent : ComponentBase
    {
        [SerializeField]
        private GameObject _cars, _carPaints, _carParts;

        [SerializeField]
        private CarVisualizer _carVisualizer; // temp

        private void Awake()
        {
            InitPlayerItems();
        }

        public void DrawCars()
        {

        }

        public void HideCars()
        {

        }

        public void DrawCarPaints()
        {
            _carPaints.SetActive(true);
        }

        public void HideCarPaints()
        {
            _carPaints.SetActive(false);
        }

        public void DrawCarParts()
        {
            _carParts.SetActive(true);
        }

        public void HideCarParts()
        {
            _carParts.SetActive(false);
        }

        public void HideAllContainers()
        {
            _cars.SetActive(false);
            _carPaints.SetActive(false);
            _carParts.SetActive(false);
        }

        private void InitPlayerItems()
        {
            var playerData = SaveSystem.PlayerData;
            var paints = DataLibrary.Instance.CarPaintsData;
            var cars = DataLibrary.Instance.CarsData;

            foreach (var unlockedPaint in playerData.UnlockedPaints)
            {
                var paintData = paints[unlockedPaint];
            }
        }
    }
}
using Assets.Scripts.Car;
using Assets.Scripts.Helpers;
using Assets.Scripts.SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GarageComponent : ComponentBase
    {
        [SerializeField]
        private GameObject _cars, _carPaints, _carParts;

        [SerializeField]
        private GameObject _garageItemPrefab;

        [SerializeField]
        private GarageManager _garageManager;

        [SerializeField]
        private Button _playButton;

        private CarSaveData _playerCarData;

        private Dictionary<CarTypeEnum, GarageItemSubcomponent> _carsComponents = new();
        private Dictionary<CarPaintTypeEnum, GarageItemSubcomponent> _carPaintsComponents = new();

        private void Awake()
        {
            InitPlayerItems();
            _playButton.onClick.AddListener(() => SaveSystem.SavePlayerData());
        }

        public void DrawCars()
        {
            _cars.SetActive(true);
        }

        public void HideCars()
        {
            _cars.SetActive(false);
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
            HideCarPaints();
            HideCars();
            HideCarParts();
        }

        private void InitPlayerItems()
        {
            var playerData = SaveSystem.PlayerData;
            _playerCarData = playerData.CarData;
            var paints = DataLibrary.Instance.CarPaintsData;
            var cars = DataLibrary.Instance.CarsData;

            foreach (var unlockedPaint in playerData.UnlockedPaints)
            {
                var data = paints[unlockedPaint];
                var instance = Instantiate(_garageItemPrefab, _carPaints.transform).GetComponent<GarageItemSubcomponent>();
                instance.Name.text = data.Name.ToString();
                instance.Image.sprite = data.Icon;
                instance.SetSelection(_playerCarData.Paint == unlockedPaint);
                instance.Button.onClick.AddListener(() =>
                {
                    _carPaintsComponents[_playerCarData.Paint].SetSelection(false);
                    _carPaintsComponents[unlockedPaint].SetSelection(true);
                    _garageManager.ChangePaint(unlockedPaint);
                });
                _carPaintsComponents.Add(unlockedPaint, instance);
            }

            foreach (var unlockedCar in playerData.UnlockedCars)
            {
                var data = cars[unlockedCar];
                var instance = Instantiate(_garageItemPrefab, _cars.transform).GetComponent<GarageItemSubcomponent>();
                instance.Name.text = data.Name.ToString();
                instance.Image.sprite = data.Icon;
                instance.SetSelection(_playerCarData.Car == unlockedCar);
                instance.Button.onClick.AddListener(() =>
                {
                    _carsComponents[_playerCarData.Car].SetSelection(false);
                    _carsComponents[unlockedCar].SetSelection(true);
                    _garageManager.ChangeCar(unlockedCar);
                });
                _carsComponents.Add(unlockedCar, instance);
            }
        }
    }
}
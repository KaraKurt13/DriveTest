using Assets.Scripts.Car;
using Assets.Scripts.Helpers;
using Assets.Scripts.Main;
using Assets.Scripts.SaveData;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageManager : MonoBehaviour
{
    public GameEngine Engine;

    [SerializeField]
    private Transform _garageSpawnPoint;

    private GameObject _playerCar;

    private CarVisualizer _visualizer;

    private CarSaveData _playerCarData;

    private void Start()
    {
        InitializePlayer();
    }

    public void Finish()
    {
        if (GameEngine.IsMultiplayerGame)
        {
            Engine.InitializeOnlinePlayer();
            Destroy(_playerCar);
        }
        else
            Engine.InitializePlayer(_playerCar);

        Engine.SwitchToStreet();
    }

    public void InitializePlayer()
    {
        _playerCarData = SaveSystem.PlayerData.CarData;
        ChangeCar(_playerCarData.Car);
        ChangePaint(_playerCarData.Paint);
        if (_playerCarData.HoodPart != CarPartTypeEnum.None)
            ChangePart(PartPlacement.Hood, _playerCarData.HoodPart);
        if (_playerCarData.BootPart != CarPartTypeEnum.None)
            ChangePart(PartPlacement.Boot, _playerCarData.BootPart);
    }

    public void ChangeCar(CarTypeEnum type)
    {
        if (_playerCar != null)
        {
            Destroy(_playerCar);
            _visualizer = null;
        }

        var carData = DataLibrary.Instance.CarsData[type];
        var carPrefab = carData.Prefab;
        _playerCar = Instantiate(carPrefab, _garageSpawnPoint.position, Quaternion.identity);

        _visualizer = _playerCar.GetComponent<CarVisualizer>();
        _playerCarData.Car = type;
        ChangePaint(_playerCarData.Paint);
    } 

    public void ChangePaint(CarPaintTypeEnum type)
    {
        var paintData = DataLibrary.Instance.CarPaintsData[type];
        _visualizer.ApplyPaint(paintData.Material);
        _playerCarData.Paint = type;
    }

    public void ChangePart(PartPlacement placement, CarPartTypeEnum type)
    {
        _visualizer.ApplyPart(type, placement);
        switch (placement)
        {
            case PartPlacement.Boot:
                _playerCarData.BootPart = type;
                break;
            case PartPlacement.Hood:
                _playerCarData.HoodPart = type;
                break;
        }
    }
}

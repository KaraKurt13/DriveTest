using Assets.Scripts.Car;
using Assets.Scripts.Helpers;
using Assets.Scripts.Main;
using Assets.Scripts.SaveData;
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
        Engine.InitializePlayerCar(_playerCar);
        Engine.SwitchToStreet();
        Engine.StartGame();
    }

    public void InitializePlayer()
    {
        _playerCarData = SaveSystem.PlayerData.CarData;
        ChangeCar(_playerCarData.Car);
        ChangePaint(_playerCarData.Paint);
    }

    public void ChangeCar(CarTypeEnum type)
    {
        if (_playerCar != null)
        {
            Destroy(_playerCar);
            _visualizer = null;
        }

        var carPrefab = DataLibrary.Instance.CarsData[type].Prefab;
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
}

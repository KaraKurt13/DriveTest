using Assets.Scripts.Car;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SaveData
{
    public class PlayerSaveData
    {
        private int _cash;
        public int Cash
        {
            get
            {
                return _cash;
            }

            set
            {
                _cash = value;
                OnCurrencyUpdate?.Invoke();
            }
        }

        private int _gold;
        public int Gold
        {
            get
            {
                return _gold;
            }

            set
            {
                _gold = value;
                OnCurrencyUpdate?.Invoke();
            }
        }

        public CarSaveData CarData;

        [JsonIgnore]
        public Action OnCurrencyUpdate;

        public HashSet<CarPaintTypeEnum> UnlockedPaints;

        public HashSet<CarTypeEnum> UnlockedCars;

        public PlayerSaveData()
        {
            Cash = 100;
            Gold = 0;
            CarData = new();
            UnlockedPaints = new() { CarPaintTypeEnum.Yellow };
            UnlockedCars = new() { CarTypeEnum.BWM };
        }
    }
}
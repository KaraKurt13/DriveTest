using Assets.Scripts.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SaveData
{
    public class PlayerSaveData
    {
        public int Money;

        public CarSaveData CarData;

        public List<CarPaintTypeEnum> UnlockedPaints;

        public PlayerSaveData()
        {
            Money = 100;
            CarData = new();
            UnlockedPaints = new() { CarPaintTypeEnum.Yellow };
        }
    }
}
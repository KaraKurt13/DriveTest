using Assets.Scripts.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SaveData
{
    public class CarSaveData
    {
        public CarPaintTypeEnum Paint;

        public CarTypeEnum Car;

        public CarSaveData()
        {
            Paint = CarPaintTypeEnum.Yellow;
            Car = CarTypeEnum.BWM;
        }
    }
}
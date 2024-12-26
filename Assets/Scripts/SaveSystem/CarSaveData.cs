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

        public CarPartTypeEnum BootPart, HoodPart;

        public CarSaveData()
        {
            Paint = CarPaintTypeEnum.Yellow;
            Car = CarTypeEnum.BWM;
            BootPart = CarPartTypeEnum.None;
            HoodPart = CarPartTypeEnum.None;
        }

        public CarPartTypeEnum GetPart(PartPlacement placement)
        {
            switch (placement)
            {
                case PartPlacement.Boot:
                    return BootPart;
                case PartPlacement.Hood:
                    return HoodPart;
            }
            return CarPartTypeEnum.None;
        }
    }
}
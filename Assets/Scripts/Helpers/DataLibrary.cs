using Assets.Scripts.Car;
using Assets.Scripts.Shop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class DataLibrary
    {
        private static DataLibrary _instance;

        public static DataLibrary Instance
        {
            get
            {
                if ( _instance == null)
                {
                    _instance = new();
                }

                return _instance;
            }
        }

        public Dictionary<CarTypeEnum, CarItem> CarData;

        public Dictionary<CarPaintTypeEnum, CarPaintItem> CarPaintData;

        private DataLibrary() { }

        public void InitData()
        {
            var paintsData = Resources.LoadAll<CarPaintItem>("ItemsData/Paints");
            CarPaintData = new();
            foreach (var paint in paintsData)
                CarPaintData.Add(paint.PaintType, paint);

            var carsData = Resources.LoadAll<CarItem>("ItemsData/Cars");
            CarData = new();
            foreach (var car in carsData)
                CarData.Add(car.CarType, car);
        }
    }
}
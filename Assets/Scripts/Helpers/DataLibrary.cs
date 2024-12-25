using Assets.Scripts.Car;
using Assets.Scripts.Shop;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public Dictionary<CarTypeEnum, CarItem> CarsData;

        public Dictionary<CarPaintTypeEnum, CarPaintItem> CarPaintsData;

        private DataLibrary() { }

        public void InitData()
        {
            var paintsData = Resources.LoadAll<CarPaintItem>("ItemsData/Paints");
            CarPaintsData = new();
            foreach (var paint in paintsData)
                CarPaintsData.Add(paint.PaintType, paint);

            var carsData = Resources.LoadAll<CarItem>("ItemsData/Cars");
            CarsData = new();
            foreach (var car in carsData)
                CarsData.Add(car.CarType, car);
        }
    }
}
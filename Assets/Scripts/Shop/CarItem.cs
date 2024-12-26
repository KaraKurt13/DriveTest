using Assets.Scripts.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    [CreateAssetMenu(fileName = "Car", menuName = "Shop/Car")]
    public class CarItem : ItemData
    {
        public override ItemTypeEnum Type => ItemTypeEnum.CAR_ITEM;

        public CarTypeEnum CarType;

        public GameObject Prefab;
    }
}
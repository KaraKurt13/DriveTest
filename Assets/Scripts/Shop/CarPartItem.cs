using Assets.Scripts.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    [CreateAssetMenu(fileName = "CarPart", menuName = "Shop/CarPart")]
    public class CarPartItem : ItemData
    {
        public override ItemTypeEnum Type => ItemTypeEnum.CAR_PART;

        public GameObject Prefab;

        public CarPartTypeEnum PartType;

        public PartPlacement Placement;
    }
}
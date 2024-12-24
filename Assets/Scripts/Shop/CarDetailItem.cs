using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    [CreateAssetMenu(fileName = "CarDetail", menuName = "Shop/CarDetail")]
    public class CarDetailItem : ItemData
    {
        public override ItemTypeEnum Type => ItemTypeEnum.CAR_DETAIL;

        public override void Apply()
        {
        }
    }
}
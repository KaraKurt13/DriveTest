using Assets.Scripts.Car;
using Assets.Scripts.SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    [CreateAssetMenu(fileName = "CarPaint", menuName = "Shop/CarPaint")]
    public class CarPaintItem : ItemData
    {
        public CarPaintTypeEnum PaintType;

        public Material Material;

        public override ItemTypeEnum Type => ItemTypeEnum.CAR_PAINT;

        public override void UnlockItem()
        {
            SaveSystem.PlayerData.UnlockedPaints.Add(PaintType);
            SaveSystem.SavePlayerData();
        }
    }
}
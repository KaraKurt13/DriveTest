using Assets.Scripts.Car;
using Assets.Scripts.SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    public class ShopManager : MonoBehaviour
    {
        private PlayerSaveData _playerData => SaveSystem.PlayerData;

        public void BuyPaint(CarPaintItem paint)
        {
            var price = paint.Price;
            _playerData.UnlockedPaints.Add(paint.PaintType);
            _playerData.Cash -= price;
            SaveSystem.SavePlayerData();
        }

        public void BuyCar(CarItem car)
        {
            var price = car.Price;
            _playerData.UnlockedCars.Add(car.CarType);
            _playerData.Cash -= price;
            SaveSystem.SavePlayerData();
        }

        public void BuyCarDetail(CarPartItem part)
        {
            var price = part.Price;
            _playerData.UnlockedPart.Add(part.PartType);
            _playerData.Cash -= price;
            SaveSystem.SavePlayerData();
        }

        public void ExchangeGoldToCash()
        {

        }

        public void ExchangeCashToGold()
        {

        }
    }
}
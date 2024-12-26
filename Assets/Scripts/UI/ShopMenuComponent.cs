using Assets.Scripts.Helpers;
using Assets.Scripts.SaveData;
using Assets.Scripts.Shop;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ShopMenuComponent : ComponentBase
    {
        [SerializeField]
        private Transform _carsContainer, _carPartsContainer, _carPaintsContainer;

        [SerializeField]
        private GameObject _cars, _carParts, _carPaints;

        [SerializeField]
        private GameObject _shopItemPrefab;

        [SerializeField]
        private ShopManager _shopManager;

        [SerializeField]
        private TextMeshProUGUI _cashAmount, _goldAmount;

        private PlayerSaveData _playerSaveData;

        public override void Draw()
        {
            HideAllContainers();
            DrawCarPaints();
            UpdateCurrency();
            _playerSaveData.OnCurrencyUpdate += RedrawShop;
            base.Draw();
        }

        public override void Hide()
        {
            _playerSaveData.OnCurrencyUpdate -= RedrawShop;
            base.Hide();
        }

        public void Initialize()
        {
            _playerSaveData = SaveSystem.PlayerData;
        }

        public void DrawCarParts()
        {
            _carParts.SetActive(true);
            RedrawCarParts();
        }

        public void DrawCarPaints()
        {
            _carPaints.SetActive(true);
            RedrawCarPaints();
        }

        public void DrawCars()
        {
            _cars.SetActive(true);
            RedrawCars();
        }

        public void RedrawShop()
        {
            UpdateCurrency();
            if (_cars.activeSelf)
                RedrawCars();
            if (_carPaints.activeSelf)
                RedrawCarPaints();
            if (_carParts.activeSelf)
                RedrawCarParts();
        }

        public void UpdateCurrency()
        {
            _cashAmount.text = $"{_playerSaveData.Cash} $";
            _goldAmount.text = $"{_playerSaveData.Gold} G";
        }

        public void HideAllContainers()
        {
            _cars.SetActive(false);
            _carParts.SetActive(false);
            _carPaints.SetActive(false);
        }

        private void RedrawCarPaints()
        {
            ClearContainer(_carPaintsContainer);

            var paintsData = DataLibrary.Instance.CarPaintsData;

            foreach (var paint in paintsData)
            {
                var shopItem = Instantiate(_shopItemPrefab, _carPaintsContainer).GetComponent<ShopItemSubcomponent>();
                var data = paint.Value;
                var type = paint.Key;

                shopItem.Name.text = data.Name;
                shopItem.Price.text = $"{data.Price}$";
                shopItem.Icon.sprite = data.Icon;
                shopItem.BuyButton.onClick.AddListener(() =>
                {
                    _shopManager.BuyPaint(data);
                });
                if (_playerSaveData.UnlockedPaints.Contains(type))
                {
                    shopItem.UpdatePurchasedStatus(true);
                }
                else
                {
                    var canBuy = data.Price <= _playerSaveData.Cash;
                    shopItem.UpdateAvailabilityStatus(canBuy);
                }
            }
        }

        private void RedrawCars()
        {
            ClearContainer(_carsContainer);
            
            var carsData = DataLibrary.Instance.CarsData;

            foreach (var car in carsData)
            {
                var shopItem = Instantiate(_shopItemPrefab, _carsContainer).GetComponent<ShopItemSubcomponent>();
                var data = car.Value;
                var type = car.Key;

                shopItem.Name.text = data.Name;
                shopItem.Price.text = $"{data.Price}$";
                shopItem.Icon.sprite = data.Icon;
                shopItem.BuyButton.onClick.AddListener(() =>
                {
                    _shopManager.BuyCar(data);
                });
                if (_playerSaveData.UnlockedCars.Contains(type))
                {
                    shopItem.UpdatePurchasedStatus(true);
                }
                else
                {
                    var canBuy = data.Price <= _playerSaveData.Cash;
                    shopItem.UpdateAvailabilityStatus(canBuy);
                }
            }
        }

        private void RedrawCarParts()
        {
            ClearContainer(_carPartsContainer);

            var partsData = DataLibrary.Instance.CarPartsData;

            foreach (var part in partsData)
            {
                var shopItem = Instantiate(_shopItemPrefab, _carPartsContainer).GetComponent<ShopItemSubcomponent>();
                var data = part.Value;
                var type = part.Key;

                shopItem.Name.text = data.Name;
                shopItem.Price.text = $"{data.Price}$";
                shopItem.Icon.sprite = data.Icon;
                shopItem.BuyButton.onClick.AddListener(() =>
                {
                    _shopManager.BuyCarDetail(data);
                });
                if (_playerSaveData.UnlockedPart.Contains(type))
                {
                    shopItem.UpdatePurchasedStatus(true);
                }
                else
                {
                    var canBuy = data.Price <= _playerSaveData.Cash;
                    shopItem.UpdateAvailabilityStatus(canBuy);
                }
            }
        }

        private void ClearContainer(Transform container)
        {
            foreach (Transform child in container)
                Destroy(child.gameObject);
        }

        private void OnDestroy()
        {
            _playerSaveData.OnCurrencyUpdate -= UpdateCurrency;
        }
    }
}
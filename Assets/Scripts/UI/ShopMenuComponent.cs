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
            foreach (Transform child in _carPaintsContainer)
                Destroy(child.gameObject);

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
                    RedrawCarPaints();
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

        }

        private void RedrawCarParts()
        {

        }

        private void OnDestroy()
        {
            _playerSaveData.OnCurrencyUpdate -= UpdateCurrency;
        }
    }
}
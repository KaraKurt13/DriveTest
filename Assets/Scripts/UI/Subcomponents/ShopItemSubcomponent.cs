using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ShopItemSubcomponent : MonoBehaviour
    {
        public TextMeshProUGUI Name, Price;

        public Image Icon;

        public Button BuyButton;

        [SerializeField]
        private GameObject _alreadyPurchasedCover, _notEnoughCurrecyObject;

        public void UpdatePurchasedStatus(bool isBought)
        {
            BuyButton.gameObject.SetActive(!isBought);
            _alreadyPurchasedCover.SetActive(isBought);
            if (isBought)
                _notEnoughCurrecyObject.SetActive(false);
        }

        public void UpdateAvailabilityStatus(bool isAvailable)
        {
            _notEnoughCurrecyObject.SetActive(!isAvailable);
            BuyButton.gameObject.SetActive(isAvailable);
        }
    }
}
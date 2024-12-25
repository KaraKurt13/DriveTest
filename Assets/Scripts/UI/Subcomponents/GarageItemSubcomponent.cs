using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GarageItemSubcomponent : MonoBehaviour
    {
        public Image Image;

        public TextMeshProUGUI Name;

        public Button Button;

        public GameObject SelectedOverlayImage;

        public void SetSelection(bool isSelected)
        {
            Button.enabled = !isSelected;
            SelectedOverlayImage.SetActive(isSelected);
        }
    }
}
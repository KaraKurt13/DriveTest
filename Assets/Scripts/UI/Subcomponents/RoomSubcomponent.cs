using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class RoomSubcomponent : MonoBehaviour
    {
        public TextMeshProUGUI Name, PlayersCount;

        public Button Button;

        [SerializeField]
        private Image _background;

        [SerializeField]
        private Color _selectedColor, _defaultColor;

        public void UpdateSelection(bool isSelected)
        {
            var color = isSelected ? _selectedColor : _defaultColor;
            _background.color = color;
        }
    }
}
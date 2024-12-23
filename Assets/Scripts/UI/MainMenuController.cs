using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _mainPanel, _modeSelectionPanel;

        public ShopMenuComponent ShopMenuComponent;

        public SettingsComponent SettingsComponent;

        public MultiplayerWindowComponent MultiplayerWindowComponent;

        public void DrawMailPanel()
        {
            _mainPanel.SetActive(true);
        }

        public void HideMainPanel()
        {
            _mainPanel.SetActive(false);
        }

        public void DrawModeSelection()
        {
            _modeSelectionPanel.SetActive(true);
        }

        public void HideModeSelection()
        {
            _modeSelectionPanel.SetActive(false);
        }
    }
}
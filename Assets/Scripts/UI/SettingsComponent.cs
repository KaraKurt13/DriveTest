using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class SettingsComponent : ComponentBase
    {
        [SerializeField]
        private TMP_Dropdown _resolutionsDropdown;

        [SerializeField]
        private Toggle _fullScreenToggle;

        private Resolution[] _resolutions;

        public void Initialize()
        {
            InitResolutions();
            InitToggle();

        }

        private void InitResolutions()
        {
            _resolutions = Screen.resolutions;
            _resolutionsDropdown.ClearOptions();
            var options = new List<string>();
            var currentResolutionIndex = 0;

            for (int i = 0; i < _resolutions.Length; i++)
            {
                var option = $"{_resolutions[i].width} x {_resolutions[i].height}";
                options.Add(option);
                if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            _resolutionsDropdown.AddOptions(options);
            _resolutionsDropdown.value = currentResolutionIndex;
            _resolutionsDropdown.RefreshShownValue();
        }

        private void InitToggle()
        {
            _fullScreenToggle.isOn = Screen.fullScreen;
        }

        public void SetResolution(int index)
        {
            var resolution = _resolutions[index];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        public void SetFullscreen(bool fullScreen)
        {
            Screen.fullScreen = fullScreen;
        }
    }
}
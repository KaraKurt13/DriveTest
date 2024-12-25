using Assets.Scripts.Helpers;
using Assets.Scripts.SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Main
{
    public class MainMenuEngine : MonoBehaviour
    {
        public MultiplayerManager MultiplayerManager;

        private void Awake()
        {
            DataLibrary.Instance.InitData();
            SaveSystem.LoadPlayerData();
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
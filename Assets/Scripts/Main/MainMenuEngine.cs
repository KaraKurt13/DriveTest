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
            MultiplayerManager.Initialize();
        }
    }
}
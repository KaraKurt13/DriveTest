using Assets.Scripts.Main;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class WaitingScreenSubcomponent : ComponentBase
    {
        [SerializeField]
        private TextMeshProUGUI Text;

        private void Update()
        {
            UpdateText();
        }

        public void UpdateText()
        {
            var playersCount = PhotonNetwork.CurrentRoom.PlayerCount;
            var readyPlayers = GameEngine.GetReadyPlayersAmount();
            Text.text = $"Waiting for players... ({readyPlayers}/{playersCount})";
        }
    }
}
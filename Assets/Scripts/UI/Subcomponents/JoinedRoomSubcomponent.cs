using Assets.Scripts.Main;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class JoinedRoomSubcomponent : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private TextMeshProUGUI _roomName;

        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private Transform _playersContainer;

        [SerializeField]
        private GameObject _playerPrefab;

        public void Draw()
        {
            var room = PhotonNetwork.CurrentRoom;
            _roomName.text = room.Name;
            _startButton.enabled = PhotonNetwork.IsMasterClient;

            UpdatePlayersList();
            
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void LeaveCurrentRoom()
        {
            MultiplayerManager.Instance.LeaveRoom();
            ClearPlayerList();
            Hide();
        }


        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            UpdatePlayersList();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            UpdatePlayersList();
        }

        public void UpdatePlayersList()
        {
            ClearPlayerList();
            var playerList = PhotonNetwork.PlayerList;
            foreach (var player in playerList)
            {
                var playerObject = Instantiate(_playerPrefab, _playersContainer).GetComponent<PlayerInRoomSubcomponent>();
                playerObject.Name.text = player.NickName;
            }
        }

        private void ClearPlayerList()
        {
            foreach (Transform player in _playersContainer)
            {
                Destroy(player.gameObject);
            }
        }
    }
}
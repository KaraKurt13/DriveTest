using Assets.Scripts.Main;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MultiplayerWindowComponent : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private Transform _roomsContainer;

        [SerializeField]
        private GameObject _roomComponentPrefab;

        [SerializeField]
        private GameObject _roomSelectionWindow, _roomCreationWindow;

        [SerializeField]
        private JoinedRoomSubcomponent _joinedRoomComponent;

        private string _selectedRoomName;
        private RoomSubcomponent _selectedRoom;

        public void Draw()
        {
            _roomSelectionWindow.SetActive(true);
            MultiplayerManager.Instance.RoomsListUpdated += UpdateRoomList;
            UpdateRoomList();
        }

        public void DrawRoomCreationWindow()
        {
            _roomCreationWindow.SetActive(true);
        }

        public void Hide()
        {
            MultiplayerManager.Instance.RoomsListUpdated -= UpdateRoomList;
            _roomSelectionWindow.SetActive(false);
        }

        private void UpdateRoomList()
        {
            ClearRoomList();
            ResetRoomSelection();

            var roomsData = MultiplayerManager.Instance.Rooms;
            foreach (var room in roomsData)
            {
                var roomObject = Instantiate(_roomComponentPrefab, _roomsContainer).GetComponent<RoomSubcomponent>();
                var roomName = room.Name;
                roomObject.Name.text = roomName;
                roomObject.Button.onClick.AddListener(() => 
                {
                    SelectRoomForJoining(roomName);
                    _selectedRoom?.UpdateSelection(false);
                    _selectedRoom = roomObject;
                    roomObject.UpdateSelection(true);
                });
                roomObject.PlayersCount.text = $"{room.PlayerCount} / {room.MaxPlayers}";
            }   
        }

        private void ClearRoomList()
        {
            foreach (Transform room in _roomsContainer)
            {
                Destroy(room.gameObject);
            }
        }

        public void SelectRoomForJoining(string roomName)
        {
            _selectedRoomName = roomName;
        }

        public void JoinSelectedRoom()
        {
            if (string.IsNullOrEmpty(_selectedRoomName))
                return;

            MultiplayerManager.Instance.JoinRoom(_selectedRoomName);
        }

        public override void OnJoinedRoom()
        {
            _joinedRoomComponent.Draw();
            _roomSelectionWindow.gameObject.SetActive(false);
        }

        private void ResetRoomSelection()
        {
            _selectedRoomName = string.Empty;
            _selectedRoom = null;
        }
    }
}
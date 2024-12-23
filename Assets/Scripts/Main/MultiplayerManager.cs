using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Main
{
    public class MultiplayerManager : MonoBehaviourPunCallbacks
    {
        public static MultiplayerManager Instance { get; private set; }

        public List<RoomInfo> Rooms { get; private set; } = new();

        public Action RoomsListUpdated { get; set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            ConnectToPhoton();
        }

        public void ConnectToPhoton()
        {
            if (!PhotonNetwork.IsConnected)
            {
                Debug.Log("Connecting to Photon...");
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnConnectedToMaster()
        {
            JoinLobby();
        }

        public void JoinLobby()
        {
            PhotonNetwork.JoinLobby();
            Debug.Log("Joining lobby");
        }

        #region RoomsManagement
        public void JoinRoom(string roomName)
        {
            PhotonNetwork.JoinRoom(roomName);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public void CreateRoom(string roomName, int maxPlayers)
        {
            RoomOptions roomOptions = new()
            {
                MaxPlayers = maxPlayers,
                IsVisible = true,
                IsOpen = true,
            };
            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach (var room in roomList)
            {
                if (room.RemovedFromList)
                {
                    Rooms.Remove(room);
                    continue;
                }
                
                if (!Rooms.Contains(room))
                    Rooms.Add(room);
            }
            RoomsListUpdated?.Invoke();
        }
        #endregion RoomsManagement
    }
}

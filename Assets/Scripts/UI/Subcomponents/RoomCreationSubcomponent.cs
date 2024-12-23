using Assets.Scripts.Main;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class RoomCreationSubcomponent : ComponentBase
    {
        [SerializeField]
        private TMP_InputField _roomName;

        public void InvokeRoomCreation()
        {
            var roomName = _roomName.text;
            if (string.IsNullOrEmpty(roomName))
            {
                Debug.Log("Can't create room with empty name!");
                return;
            }

            MultiplayerManager.Instance.CreateRoom(roomName, 4);
            Hide();
        }

        public override void Draw()
        {
            _roomName.text = string.Empty;
            base.Draw();
        }
    }
}
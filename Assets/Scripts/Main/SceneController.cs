using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Main
{
    public class SceneController : MonoBehaviour
    {
        public void LoadOnlineGame()
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            try
            {
                PhotonNetwork.LoadLevel("MultiplayerScene");
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

        }

        public void LoadSingleplayerGame()
        {
            SceneManager.LoadScene("SingleplayerScene");
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
using Photon.Pun;
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
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel(1);
            }
        }

        public void LoadSingleplayerGame()
        {
            SceneManager.LoadScene(1);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
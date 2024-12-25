using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Main
{
    public class SceneController : MonoBehaviour
    {
        public static SceneController Instance { get; private set; }

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
        }

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
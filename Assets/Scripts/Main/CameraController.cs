using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Main
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Camera _carCamera, _garageCamera;

        public void SetCarCamera(Camera carCamera)
        {
            _carCamera = carCamera;
        }

        public void SwitchToGarageCamera()
        {
            _garageCamera.enabled = true;
            _carCamera.enabled = false;
            _carCamera.GetComponent<AudioListener>().enabled = false;
            _garageCamera.GetComponent<AudioListener>().enabled = true;
        }

        public void SwitchToCarCamera()
        {
            _carCamera.enabled = true;
            _garageCamera.enabled = false;
            _carCamera.GetComponent<AudioListener>().enabled = true;
            _garageCamera.GetComponent<AudioListener>().enabled = false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Main
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Camera _carCamera, _garageCamera;

        public void SwitchToGarageCamera()
        {
            _garageCamera.enabled = true;
            _carCamera.enabled = false;
        }

        public void SwitchToCarCamera()
        {
            _carCamera.enabled = true;
            _garageCamera.enabled = false;
        }
    }
}
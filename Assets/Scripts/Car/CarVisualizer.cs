using Assets.Scripts.Helpers;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Car
{
    public class CarVisualizer : MonoBehaviour
    {
        public Camera CarCamera;

        [SerializeField]
        private MeshRenderer _meshRenderer;

        public void ApplyPaint(Material material)
        {
            _meshRenderer.material = material;
        }

        [PunRPC]
        public void ApplyPaintPhoton(CarPaintTypeEnum paint)
        {
            var material = DataLibrary.Instance.CarPaintsData[paint].Material;
            _meshRenderer.material = material;
        }
    }
}
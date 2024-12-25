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
    }
}
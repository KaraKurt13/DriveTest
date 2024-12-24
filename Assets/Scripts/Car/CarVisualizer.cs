using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Car
{
    public class CarVisualizer : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer _meshRenderer;
        
        public void ChangeMaterial(Material material)
        {
            _meshRenderer.materials[0] = material;
        }
    }
}
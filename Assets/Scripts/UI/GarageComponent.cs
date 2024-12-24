using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GarageComponent : ComponentBase
    {
        [SerializeField]
        private GameObject _carMaterials, _carDetails;

        public void DrawCarPaints()
        {
            _carMaterials.SetActive(true);
        }

        public void HideCarPaints()
        {
            _carMaterials.SetActive(false);
        }

        public void DrawCarDetails()
        {
            _carDetails.SetActive(true);
        }

        public void HideCarDetails()
        {
            _carDetails.SetActive(false);
        }
    }
}
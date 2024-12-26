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

        [SerializeField]
        private Transform _boot, _hood;

        private GameObject _bootObject, _hoodObject;

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

        public void ApplyPart(CarPartTypeEnum part, PartPlacement partPlacement)
        {
            var partPrefab = DataLibrary.Instance.CarPartsData[part].Prefab;

            switch (partPlacement)
            {
                case PartPlacement.Boot:
                    if (_bootObject != null)
                        Destroy(_bootObject);
                    _bootObject = Instantiate(partPrefab, transform, false);
                    _bootObject.transform.localPosition = _boot.localPosition;
                    break;
                case PartPlacement.Hood:
                    if (_hoodObject != null)
                        Destroy(_hoodObject);
                    _hoodObject = Instantiate(partPrefab, transform, false);
                    _hoodObject.transform.localPosition = _hood.localPosition;
                    break;
            }
        }

        [PunRPC]
        public void ApplyPartPhoton(CarPartTypeEnum part, PartPlacement partPlacement)
        {
            var partPrefab = DataLibrary.Instance.CarPartsData[part].Prefab;

            switch (partPlacement)
            {
                case PartPlacement.Boot:
                    if (_bootObject != null)
                        Destroy(_bootObject);
                    _bootObject = Instantiate(partPrefab, transform, false);
                    _bootObject.transform.localPosition = _boot.localPosition;
                    break;
                case PartPlacement.Hood:
                    if (_hoodObject != null)
                        Destroy(_hoodObject);
                    _hoodObject = Instantiate(partPrefab, transform, false);
                    _hoodObject.transform.localPosition = _hood.localPosition;
                    break;
            }
        }
    }
}
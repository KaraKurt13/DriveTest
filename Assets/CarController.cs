using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    WheelCollider[] _colliders;

    float _torque = 100;

    float _angle = 45f;

    private void Update()
    {
        foreach (var collider in _colliders)
        {
            collider.motorTorque = Input.GetAxis("Vertical") * _torque;
        }
    }
}

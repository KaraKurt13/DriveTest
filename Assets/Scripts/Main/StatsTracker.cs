using Assets.Scripts.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsTracker : MonoBehaviour
{
    [SerializeField]
    private CarController _carController;

    [HideInInspector]
    public int Score;

    [HideInInspector]
    public float CarSpeed;

    private const int _scorePerTick = 1;

    private void FixedUpdate()
    {
        Track();
    }

    private void Track()
    {
        if (_carController.IsDrifting)
            Score++;

        CarSpeed = _carController.Rigidbody.velocity.magnitude;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Car
{
    public class CarController : MonoBehaviour
    {
        public Transform Transform;

        public Rigidbody Rigidbody;

        public bool IsControllable = false;

        [SerializeField]
        private WheelCollider _frontLeftWheelCollider, _frontRightWheelCollider, _rearLeftWheelCollider, _rearRightWheelCollider;

        [SerializeField]
        private Transform _frontLeftWheelTransform, _frontRightWheelTransform, _rearLeftWheelTransform, _rearRightWheelTransform;

        private float _currentSteerAngle, _targetSteerAngle = 0f;

        private float _horizontalInput, _verticalInput;
        private bool _brakeInput;

        public bool IsDrifting { get; private set; }

        private const float _maxTorque = 1000, _maxSteerAngle = 30f, _brakeForce = 5000f, _wheelsRotationStep = 10f;

        private void Update()
        {
            GetInput();
            UpdateWheels();
            Debug.Log(IsDrifting);
        }

        private void FixedUpdate()
        {
            HandleMotor();
            HandleSteering();
            HandleBrake();
            HandleDrifting();
        }

        private void GetInput()
        {
            if (!IsControllable)
                return;
            _verticalInput = Input.GetAxis("Vertical");
            _horizontalInput = Input.GetAxis("Horizontal");
            _brakeInput = Input.GetKey(KeyCode.Space);
        }

        private void HandleMotor()
        {
            var motorTorque = _verticalInput * _maxTorque;
            _rearLeftWheelCollider.motorTorque = motorTorque;
            _rearRightWheelCollider.motorTorque = motorTorque;
            HandleBrake();
        }

        private void HandleSteering()
        {
            _targetSteerAngle = _horizontalInput * _maxSteerAngle;
            _currentSteerAngle = Mathf.LerpAngle(_currentSteerAngle, _targetSteerAngle, Time.deltaTime * _wheelsRotationStep);

            _frontLeftWheelCollider.steerAngle = _currentSteerAngle;
            _frontRightWheelCollider.steerAngle = _currentSteerAngle;
        }

        private void HandleBrake()
        {
            var brakeForce = _brakeInput ? _brakeForce : 0f;

            //_frontLeftWheelCollider.brakeTorque = brakeForce;
            //_frontRightWheelCollider.brakeTorque = brakeForce;
            _rearLeftWheelCollider.brakeTorque = brakeForce;
            _rearRightWheelCollider.brakeTorque = brakeForce;
        }

        private void UpdateWheels()
        {
            UpdateSingleWheel(_rearLeftWheelCollider, _rearLeftWheelTransform);
            UpdateSingleWheel(_rearRightWheelCollider, _rearRightWheelTransform);
            UpdateSingleWheel(_frontLeftWheelCollider, _frontLeftWheelTransform);
            UpdateSingleWheel(_frontRightWheelCollider, _frontRightWheelTransform);
        }

        private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
        {
            Vector3 position;
            Quaternion rotation;
            wheelCollider.GetWorldPose(out position, out rotation);
            wheelTransform.rotation = rotation;
            wheelTransform.position = position;
        }

        private void HandleDrifting()
        {
            Vector3 velocity = Rigidbody.velocity;

            float lateralVelocity = Vector3.Dot(transform.right, velocity);

            IsDrifting = Mathf.Abs(lateralVelocity) > 1f && _verticalInput != 0;
            Debug.Log(IsDrifting);
        }
    }
}
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Car
{
    public class CarController : MonoBehaviour
    {
        public Transform Transform;

        public Rigidbody Rigidbody;

        public PhotonView PhotonView;

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

        private Vector3 _forwardDirection => transform.forward.normalized;
        private Vector3 _velocityDirection => Rigidbody.velocity.normalized;

        private void Update()
        {
            GetInput();
            UpdateWheels();
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
            if (IsDrifting && _horizontalInput == 0)
            {
                AutoSteer();
            }
            else
            {
                _targetSteerAngle = _horizontalInput * _maxSteerAngle;
                _currentSteerAngle = Mathf.LerpAngle(_currentSteerAngle, _targetSteerAngle, Time.deltaTime * _wheelsRotationStep);
            }

            _frontLeftWheelCollider.steerAngle = _currentSteerAngle;
            _frontRightWheelCollider.steerAngle = _currentSteerAngle;
        }

        private void AutoSteer()
        {
            Vector3 cross = Vector3.Cross(_forwardDirection, _velocityDirection);
            float autoSteerDirection = cross.y > 0 ? -1 : 1;
            _targetSteerAngle = autoSteerDirection * _maxSteerAngle;
            _currentSteerAngle = Mathf.LerpAngle(_currentSteerAngle, _targetSteerAngle, Time.deltaTime * _wheelsRotationStep);
        }

        private void HandleBrake()
        {
            var brakeForce = _brakeInput ? _brakeForce : 0f;
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

        private float _driftThreshold = 10f;
        private void HandleDrifting()
        {
            float driftAngle = Vector3.Angle(_velocityDirection, _forwardDirection);
            IsDrifting =  driftAngle > _driftThreshold && Rigidbody.velocity.magnitude > 1f;
        }
    }
}
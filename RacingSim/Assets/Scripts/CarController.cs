using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [SerializeField] private List<AxleInfo> axleInfos;

    private InputController inputController;

    [SerializeField] private float maxEnginePower;
    [SerializeField] private float maxBrakePower;
    [SerializeField] private float maxSteeringAngle;

    private float throttle;
    private float turn;
    private float brake;

    private void Awake()
    {
        inputController = new InputController();
    }

    private void OnEnable()
    {
        inputController.Enable();
        inputController.Player.Throttle.performed += OnThrottlePerformed;
        inputController.Player.Throttle.canceled += OnThrottleCanceled;
        inputController.Player.SteeringWheel.performed += OnSteerPerformed;
        inputController.Player.SteeringWheel.canceled += OnSteerCanceled;
        inputController.Player.Brake.performed += OnBrakePerformed;
        inputController.Player.Brake.canceled += OnBrakeCanceled;
    }

    private void OnDisable()
    {
        inputController.Player.Throttle.performed -= OnThrottlePerformed;
        inputController.Player.Throttle.canceled -= OnThrottleCanceled;
        inputController.Player.SteeringWheel.performed -= OnSteerPerformed;
        inputController.Player.SteeringWheel.canceled -= OnSteerCanceled;
        inputController.Player.Brake.performed -= OnBrakePerformed;
        inputController.Player.Brake.canceled -= OnBrakeCanceled;
        inputController.Disable();
    }

    private void OnThrottlePerformed(InputAction.CallbackContext ctx) => throttle = ctx.ReadValue<float>();
    private void OnThrottleCanceled(InputAction.CallbackContext ctx) => throttle = 0f;
    private void OnSteerPerformed(InputAction.CallbackContext ctx) => turn = ctx.ReadValue<float>();
    private void OnSteerCanceled(InputAction.CallbackContext ctx) => turn = 0f;
    private void OnBrakePerformed(InputAction.CallbackContext ctx) => brake = ctx.ReadValue<float>();
    private void OnBrakeCanceled(InputAction.CallbackContext ctx) => brake = 0f;

    private void FixedUpdate()
    {
        UpdateWheelPhysics();
    }

    private void Update()
    {
        UpdateWheelVisuals();
    }

    private void UpdateWheelPhysics()
    {
        float steeringAngle = turn * maxSteeringAngle;
        float throttlePower = throttle * maxEnginePower;
        float brakePower = brake * maxBrakePower;

        foreach (AxleInfo info in axleInfos)
        {
            if (info.isSteering)
            {
                info.rightWheelCollider.steerAngle = steeringAngle;
                info.leftWheelCollider.steerAngle = steeringAngle;
            }

            if (info.isMotor)
            {
                info.rightWheelCollider.motorTorque = throttlePower;
                info.leftWheelCollider.motorTorque = throttlePower;                
            }

            info.rightWheelCollider.brakeTorque = brakePower;
            info.leftWheelCollider.brakeTorque = brakePower;
        }
    }

    private void UpdateWheelVisuals()
    {
        foreach (AxleInfo info in axleInfos)
        {
            ApplyWheelPose(info.leftWheelCollider, info.leftWheelTransform);
            ApplyWheelPose(info.rightWheelCollider, info.rightWheelTransform);
        }
    }

    private void ApplyWheelPose(WheelCollider wheelColl, Transform wheelTransform)
    {
        wheelColl.GetWorldPose(out Vector3 position, out Quaternion rotation);

        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }


    [Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheelCollider;
        public WheelCollider rightWheelCollider;
        public Transform leftWheelTransform;
        public Transform rightWheelTransform;
        public bool isMotor;
        public bool isSteering;
    }
}

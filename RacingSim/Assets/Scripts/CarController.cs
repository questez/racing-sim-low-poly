using System;
using System.Collections.Generic;
using UnityEngine;

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
        inputController.Player.Throttle.performed += ctx => throttle = ctx.ReadValue<float>();
        inputController.Player.Throttle.canceled += ctx => throttle = 0;

        inputController.Player.SteeringWheel.performed += ctx => turn = ctx.ReadValue<float>();
        inputController.Player.SteeringWheel.canceled += ctx => turn = 0;

        inputController.Player.Brake.performed += ctx => brake = ctx.ReadValue<float>();
        inputController.Player.Brake.canceled += ctx => brake = 0;
    }

    private void OnDisable()
    {
        inputController.Disable();
        inputController.Player.Throttle.performed -= ctx => throttle = ctx.ReadValue<float>();
        inputController.Player.Throttle.canceled -= ctx => throttle = 0;

        inputController.Player.SteeringWheel.performed -= ctx => turn = ctx.ReadValue<float>();
        inputController.Player.SteeringWheel.canceled -= ctx => turn = 0;

        inputController.Player.Brake.performed -= ctx => brake = ctx.ReadValue<float>();
        inputController.Player.Brake.canceled -= ctx => brake = 0;
    }

    private void UpdateWheelState()
    {
        float steeringAngle = turn * maxSteeringAngle;
        float throttlePower = throttle * maxEnginePower;
        float brakePower = brake * maxBrakePower;

        foreach (AxleInfo info in axleInfos)
        {
            if (info.isSteering)
            {
                info.rightWheel.steerAngle = steeringAngle;
                info.leftWheel.steerAngle = steeringAngle;
            }

            if (info.isMotor)
            {
                info.rightWheel.motorTorque = throttlePower;
                info.leftWheel.motorTorque = throttlePower;                
            }

            info.rightWheel.brakeTorque = brakePower;
            info.leftWheel.brakeTorque = brakePower;
        }
    }

    private void FixedUpdate()
    {
        UpdateWheelState();
    }

    [Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool isMotor;
        public bool isSteering;
    }
}

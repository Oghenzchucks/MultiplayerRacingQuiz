using System.Collections;
using Fusion;
using GameSystem;
using UnityEngine;

namespace Car
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarController : NetworkBehaviour
    {
        private float horizontalInput, verticalInput;
        private float currentSteerAngle, currentbreakForce;
        private bool isBreaking;

        // Settings
        [SerializeField] private float motorForce, breakForce, maxSteerAngle;

        // Wheel Colliders
        [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
        [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

        // Wheels
        [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
        [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

        [SerializeField] private bool isSinglePlayer;

        private Rigidbody _carRigidbody;

        private void Start()
        {
            _carRigidbody = GetComponent<Rigidbody>();
        }

        public override void Spawned()
        {
            if (HasInputAuthority)
            {
                GameLauncher.OnPlayerSpawned?.Invoke(transform);
            }
            PositionSystem.OnSpawn?.Invoke(transform, true);
        }

        private void OnDestroy()
        {
            PositionSystem.OnSpawn?.Invoke(transform, false);
        }

        private void FixedUpdate()
        {
            if (isSinglePlayer)
            {
                var direction = Vector3.zero;

                if (Input.GetKey(KeyCode.W))
                    direction += new Vector3(0, 1, 0);

                if (Input.GetKey(KeyCode.S))
                    direction += new Vector3(0, -1, 0);

                if (Input.GetKey(KeyCode.A))
                    direction += Vector3.left;

                if (Input.GetKey(KeyCode.D))
                    direction += Vector3.right;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StopCar();
                }

                GetInput(direction);
                HandleMotor();
                HandleSteering();
                UpdateWheels();
            }
        }

        public override void FixedUpdateNetwork()
        {
            if (GetInput(out NetworkInputData data))
            {
                if (HasStateAuthority)
                {
                    GetInput(data.direction);
                    HandleMotor();
                    HandleSteering();
                    UpdateWheels();
                }
            }
        }

        private void GetInput(Vector3 direction)
        {
            // Steering Input
            horizontalInput = direction.x;

            // Acceleration Input
            verticalInput = direction.y;
        }

        private void HandleMotor()
        {
            frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
            frontRightWheelCollider.motorTorque = verticalInput * motorForce;
            currentbreakForce = isBreaking ? breakForce : 0f;
            ApplyBreaking();
        }

        private void ApplyBreaking()
        {
            frontRightWheelCollider.brakeTorque = currentbreakForce;
            frontLeftWheelCollider.brakeTorque = currentbreakForce;
            rearLeftWheelCollider.brakeTorque = currentbreakForce;
            rearRightWheelCollider.brakeTorque = currentbreakForce;
        }

        private void HandleSteering()
        {
            currentSteerAngle = maxSteerAngle * horizontalInput;
            frontLeftWheelCollider.steerAngle = currentSteerAngle;
            frontRightWheelCollider.steerAngle = currentSteerAngle;
        }

        private void UpdateWheels()
        {
            UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
            UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
            UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
            UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        }

        private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
        {
            Vector3 pos;
            Quaternion rot;
            wheelCollider.GetWorldPose(out pos, out rot);
            wheelTransform.rotation = rot;
            wheelTransform.position = pos;
        }

        public void StopCar()
        {
            StartCoroutine(ApplyForceStop());
        }

        private IEnumerator ApplyForceStop()
        {
            isBreaking = true;
            ApplyBreaking();
            yield return new WaitUntil(() => _carRigidbody.velocity == Vector3.zero);
            isBreaking = false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameSystem
{
    public class PositionSystem : MonoBehaviour
    {
        [SerializeField] private Transform finishLine;
        [SerializeField] private Transform startLine;

        public List<Transform> _spawnedCars = new();
        public static Action<Transform, bool> OnSpawn;

        public int CarPosition { get; private set; }
        public int TotalCars { get; private set; }

        private Transform target;
        private bool _isActive;
        private int _lastPosition;

        private void Awake()
        {
            OnSpawn += AddSpawnedCar;
        }

        private void OnDestroy()
        {
            OnSpawn -= AddSpawnedCar;
        }

        public void SetTarget(Transform carTransform)
        {
            target = carTransform;
        }

        public void AddSpawnedCar(Transform car, bool isSpawned)
        {
            if (isSpawned)
            {
                _spawnedCars.Add(car);
            }
            else
            {
                _spawnedCars.Remove(car);
            }
        }

        public float GetCurrentPlayerDistance()
        {
            return GetCurrentDistance(target);
        }

        public float GetCurrentDistance(Transform currentTarget)
        {
            if (target == null)
            {
                return 0;
            }

            var currentDistance = GetDistanceBtwTwoPoints(currentTarget, finishLine);
            var totalDistance = GetDistanceBtwTwoPoints(startLine, finishLine);

            return totalDistance - currentDistance;
        }

        public float GetNormalizedCurrentDistance()
        {
            var totalDistance = GetDistanceBtwTwoPoints(startLine, finishLine);
            return (GetCurrentPlayerDistance() / totalDistance);
        }

        private float GetDistanceBtwTwoPoints(Transform start, Transform end)
        {
            var startPos = FormatVector3Position(start.position);
            var endPos = FormatVector3Position(end.position);
            if (startPos.z > endPos.z)
            {
                return 0;
            }
            return Vector3.Distance(startPos, endPos);
        }

        private Vector3 FormatVector3Position(Vector3 position)
        {
            return new Vector3(0, 0, position.z);
        }

        private void Update()
        {
            if (!_isActive)
            {
                return;
            }

            if (target == null)
            {
                return;
            }

            _lastPosition = CarPosition;
            CarPosition = GetPosition(target, _spawnedCars);
            TotalCars = _spawnedCars.Count;
        }

        public int GetPosition(Transform targetTransform, List<Transform> carTransforms)
        {
            float targetValue = 0;
            var index = 0;

            var distanceList = new List<float>();

            foreach (var carTransform in carTransforms)
            {
                var distance = GetCurrentDistance(carTransform);
                distanceList.Add(distance);
                if (carTransform == targetTransform)
                {
                    targetValue = distance;
                }
                index++;
            }

            int higherCount = distanceList.Count(x => x > targetValue);
            int rank = higherCount + 1;
            return rank;
        }

        public void SetPositionTrackingState(bool isActive)
        {
            _isActive = isActive;
        }

        public override string ToString()
        {
            return _lastPosition + " / " + TotalCars;
        }
    }
}

using Car;
using UnityEngine;

namespace GameSystem
{
    public class SpeedCalculator : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Transform _target;
        private CarController carController;

        private void Update()
        {
            if (_target == null)
            {
                return;
            }

            _speed = carController.CarVelocity.magnitude;
        }

        public float GetSpeed()
        {
            return _speed;
        }

        public void SetTarget(Transform followTarget)
        {
            carController = followTarget.GetComponent<CarController>();
            _target = followTarget;
        }
    }
}

using UnityEngine;

namespace GameSystem
{
    public class SpeedCalculator : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Transform _target;
        private Rigidbody carRigidbody;

        private void Update()
        {
            if (_target == null)
            {
                return;
            }

            _speed = carRigidbody.velocity.magnitude;
        }

        public float GetSpeed()
        {
            return _speed;
        }

        public void SetTarget(Transform followTarget)
        {
            carRigidbody = followTarget.GetComponent<Rigidbody>();
            _target = followTarget;
        }
    }
}

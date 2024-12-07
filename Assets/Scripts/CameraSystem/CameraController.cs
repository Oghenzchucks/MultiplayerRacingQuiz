using UnityEngine;

namespace CameraSystem
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target; 
        [SerializeField] private Vector3 offset = new Vector3(0, 5, -10);
        [SerializeField] private float smoothSpeed = 0.125f;

        public void LateUpdate()
        {
            if (target == null)
            { 
                return;
            }

            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            transform.LookAt(target.position + new Vector3(0, 2, 0));
        }

        public void SetTarget(Transform followTarget)
        {
            target = followTarget;
        }
    }
}

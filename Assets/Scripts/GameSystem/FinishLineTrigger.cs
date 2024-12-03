using Car;
using Fusion;
using UnityEngine;

namespace GameSystem
{
    public class FinishLineTrigger : NetworkBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (HasStateAuthority)
            {
                if (other.GetComponent<CarController>() != null)
                {

                }
            }
        }
    }
}

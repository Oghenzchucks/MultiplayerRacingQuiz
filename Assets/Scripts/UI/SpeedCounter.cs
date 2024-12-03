using GameSystem;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SpeedCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI speedDisplay;
        [SerializeField] private SpeedCalculator speedCalculator;

        private void Update()
        {
            speedDisplay.text = speedCalculator.GetSpeed().ToString("00.0");
        }
    }
}

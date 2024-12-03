using GameSystem;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TickCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timer;
        [SerializeField] private TimeTicker timeTicker;

        private void Update()
        {
            timer.text = TimeTicker.FormatLongTimeLeft(timeTicker.TickTimer);
        }
    }
}

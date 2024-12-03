using System;
using UnityEngine;

namespace GameSystem
{
    public class TimeTicker : MonoBehaviour
    {
        public float TickTimer { get; private set; }

        private bool _startRunning;

        private void Start()
        {
            TickTimer = 0;
        }

        private void Update()
        {
            if (!_startRunning)
            {
                return;
            }

            TickTimer += Time.deltaTime * 5;
        }

        public static string FormatLongTimeLeft(float timeLeft)
        {
            var timeSpan = TimeSpan.FromSeconds(timeLeft);
            if (timeSpan.Days >= 1)
            {
                return timeSpan.ToString("dd'd 'hh'h 'mm'm'");
            }
            return timeSpan.ToString("hh'h 'mm'm 'ss's'");
        }

        public void SetTimersState(bool isActive)
        {
            _startRunning = isActive;
        }
    }
}

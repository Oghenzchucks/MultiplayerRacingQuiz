using GameSystem;
using TMPro;
using UnityEngine;
using Utility;

namespace UI
{
    public class PositionAndDistanceView : MonoBehaviour
    {
        [SerializeField] private SlicedFilledImage distanceSlider;
        [SerializeField] private TextMeshProUGUI racePosition;
        [SerializeField] private PositionSystem positionSystem;

        private void Update()
        {
            distanceSlider.fillAmount = positionSystem.GetNormalizedCurrentDistance();
            racePosition.text = positionSystem.ToString();
        }
    }
}

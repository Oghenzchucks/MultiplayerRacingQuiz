using TMPro;
using UnityEngine;

namespace UI
{
    public class ResultView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI positionText;
        [SerializeField] private TextMeshProUGUI timerText;

        public void SetResult(string position, string timer)
        {
            positionText.text = position;
            timerText.text = timer;
        }
    }
}
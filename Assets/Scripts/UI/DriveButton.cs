using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class DriveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private bool isForward;

        public bool _isPressed;

        public bool IsPressed => _isPressed;

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressed = false;
        }
    }
}

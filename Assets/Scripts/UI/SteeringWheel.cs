using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class SteeringWheel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform parent;
        [SerializeField] private RectTransform handle;

        private bool _isDragging;
        public bool IsDragging => _isDragging;

        public void OnPointerDown(PointerEventData eventData)
        {
            _isDragging = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isDragging = false;
            handle.localPosition = Vector3.zero; 
        }

        private void Update()
        {
            if (_isDragging)
            {
                Vector2 localMousePosition;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, Input.mousePosition, null, out localMousePosition);

                // Calculate clamping bounds
                float minX = -parent.rect.width / 2;
                float maxX = parent.rect.width / 2;

                // Clamp the local position
                localMousePosition.x = Mathf.Clamp(localMousePosition.x, minX, maxX);
                localMousePosition.y = handle.localPosition.y;

                // Set the handle's position relative to the parent
                handle.localPosition = localMousePosition;
            }
        }

        public float GetNormalizedXPosition()
        {
            float minX = -parent.rect.width / 2;
            float maxX = parent.rect.width / 2;
            float currentX = handle.localPosition.x;

            // Normalize the x position to a range of -1 to 1
            return Mathf.InverseLerp(minX, maxX, currentX) * 2 - 1;
        }
    }
}

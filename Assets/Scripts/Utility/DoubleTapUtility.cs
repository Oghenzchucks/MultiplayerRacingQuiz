using MenuNavigation;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
    [RequireComponent(typeof(Button))]
    public class DoubleTapUtility : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            ActivateButton();
            MenuManager.OnMenuUpdated += MenuUpdated;
        }

        private void OnDisable()
        {
            MenuManager.OnMenuUpdated -= MenuUpdated;
        }

        private void MenuUpdated()
        {
            ActivateButton();
        }

        private void ActivateButton()
        {
            _button.interactable = true;
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace MenuNavigation
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private List<Menu> menus;

        public static Action OnMenuUpdated;
        public static Action<MenuEnums, bool> OnLoadMenu;

        private void Awake()
        {
            OnLoadMenu += SetMenu;
        }

        private void OnDestroy()
        {
            OnLoadMenu -= SetMenu;
        }

        public void SetMenu(MenuEnums menuTag, bool isActive)
        {
            foreach (var menu in menus)
            {
                menu.ShowMenu(menuTag, isActive);
            }

            OnMenuUpdated?.Invoke();
        }
    }
}

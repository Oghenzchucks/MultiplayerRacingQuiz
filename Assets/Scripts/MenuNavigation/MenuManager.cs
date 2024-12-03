using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private List<Menu> menus;

    public static Action OnMenuUpdated;

    public void SetMenu(MenuEnums menuTag, bool isActive)
    {
        foreach(var menu in menus)
        {
            menu.ShowMenu(menuTag, isActive);
        }

        OnMenuUpdated?.Invoke();
    }
}

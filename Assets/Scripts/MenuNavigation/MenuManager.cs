using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private List<Menu> menus;

    public void SetMenu(MenuEnums menuTag, bool isActive)
    {
        if (isActive)
        {
            foreach (var menu in menus)
            {
                menu.ShowMenu(menu.GetTagID == menuTag);
            }
        }
        else
        {
            var menu = menus.Find(x => x.GetTagID == menuTag);
            if (menu != null)
            {
                menu.ShowMenu(isActive);
            }
        }
    }
}

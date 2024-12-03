using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private MenuEnums tagID;
    [SerializeField] private List<Menu> subMenus;

    public MenuEnums GetTagID => tagID;

    public void ShowMenu(MenuEnums menuTagID, bool isActive)
    {
        if (menuTagID == GetTagID)
        {
            SetMenu(isActive);
        }
        else if (IsPresentInSubMenu(menuTagID))
        {
            SetSubMenu(menuTagID, isActive);
        }
        else
        {
            SetMenu(false);
        }
    }

    public void SetMenu(bool isActive)
    {
        if (subMenus.Count > 0 && isActive)
        {
            SetSubMenu(subMenus[0].GetTagID, true);
        }
        gameObject.SetActive(isActive);
    }

    private bool IsPresentInSubMenu(MenuEnums menuTagID)
    {
        return subMenus.Find(x => x.GetTagID == menuTagID) != null;
    }

    private void SetSubMenu(MenuEnums menuTagID, bool isActive)
    {
        foreach (var menu in subMenus)
        {
            if (isActive && menu.GetTagID != menuTagID)
            {
                menu.SetMenu(false);
            }
            else if (menu.GetTagID == menuTagID)
            {
                menu.SetMenu(isActive);
            }
        }

        gameObject.SetActive(true);
    }
}

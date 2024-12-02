using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private MenuEnums tagID;
    [SerializeField] private List<Menu> subMenus;

    public MenuEnums GetTagID => tagID;

    private void Start()
    {
        if (subMenus.Count > 0)
        {
            SwitchSubMenu(subMenus[0].GetTagID);
        }
    }

    public void ShowMenu(bool isActive)
    {
        if (isActive && subMenus.Count > 0)
        {
            SwitchSubMenu(subMenus[0].GetTagID);
        }
        gameObject.SetActive(isActive);
    }

    private void SwitchSubMenu(MenuEnums tagID)
    {
        foreach (var menu in subMenus)
        {
            menu.ShowMenu(menu.GetTagID == tagID);
        }
    }
}

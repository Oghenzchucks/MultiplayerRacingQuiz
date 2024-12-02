using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    [SerializeField] private MenuManager menuManager;

    public MenuManager GetMenuManager => menuManager;

    private void Start()
    {
        menuManager.SetMenu(MenuEnums.HOME, true);
    }
}

using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private MultiplayerManager multiplayerManager;
    //Test
    [SerializeField] private MenuEnums menuSwitch;

    private void Start()
    {
        menuManager.SetMenu(MenuEnums.HOME, true);
    }

    public void LaunchGame(bool isHosting)
    {
        menuManager.SetMenu(MenuEnums.LOADING_VIEW, true);
        multiplayerManager.StartGame(isHosting ? Fusion.GameMode.Host : Fusion.GameMode.Client);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            menuManager.SetMenu(menuSwitch, true);
        }
    }
}

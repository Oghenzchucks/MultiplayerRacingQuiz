using System;
using CameraSystem;
using MenuNavigation;
using Multiplayer;
using UnityEngine;

namespace GameSystem
{
    public class GameLauncher : MonoBehaviour
    {
        [SerializeField] private MultiplayerManager multiplayerManager;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private SpeedCalculator speedCalculator;
        [SerializeField] private TimeTicker timeTicker;

        public static Action<Transform> OnPlayerSpawned;

        private void Awake()
        {
            OnPlayerSpawned += PlayerSpawned;
        }

        private void Start()
        {
            ShowMenu(MenuEnums.HOME, true);
        }

        private void OnDestroy()
        {
            OnPlayerSpawned -= PlayerSpawned;
        }

        public void LaunchGame(bool isHosting)
        {
            ShowMenu(MenuEnums.LOADING_VIEW, true);
            multiplayerManager.StartGame(isHosting ? Fusion.GameMode.Host : Fusion.GameMode.Client);
        }

        public void PlayerSpawned(Transform transform)
        {
            ShowMenu(MenuEnums.HUD_VIEW, true);
            cameraController.SetTarget(transform);
            speedCalculator.SetTarget(transform);
            timeTicker.SetTimersState(true);
        }

        public void SetExitWarningScreen(bool isActive)
        {
            ShowMenu(MenuEnums.EXIT_MESSAGE, isActive);
        }

        public void ShowHudView()
        {
            ShowMenu(MenuEnums.HUD_VIEW, true);
        }

        private void ShowMenu(MenuEnums menuTagID, bool isActive)
        {
            MenuManager.OnLoadMenu?.Invoke(menuTagID, isActive);
        }
    }
}

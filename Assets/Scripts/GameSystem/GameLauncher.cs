using System;
using CameraSystem;
using MenuNavigation;
using Multiplayer;
using UI;
using UnityEngine;

namespace GameSystem
{
    public class GameLauncher : MonoBehaviour
    {
        [SerializeField] private MultiplayerManager multiplayerManagerPrefab;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private SpeedCalculator speedCalculator;
        [SerializeField] private TimeTicker timeTicker;
        [SerializeField] private PositionSystem positionSystem;
        [SerializeField] private ResultView resultView;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private InputController inputController;

        public static Action<Transform> OnPlayerSpawned;

        private PlayerController _playerController;
        private MultiplayerManager _multiplayerManager;

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
            if (_multiplayerManager == null)
            {
                _multiplayerManager = Instantiate(multiplayerManagerPrefab);
                _multiplayerManager.Initialize(inputController, spawnPoint);
            }
            _multiplayerManager.StartGame(isHosting ? Fusion.GameMode.Host : Fusion.GameMode.Shared);
        }

        public void PlayerSpawned(Transform carTransform)
        {
            cameraController.SetTarget(carTransform);
            speedCalculator.SetTarget(carTransform);
            positionSystem.SetTarget(carTransform);

            _playerController = carTransform.GetComponent<PlayerController>();
            _playerController.OnRaceFinished += ShowResultView;

            positionSystem.SetPositionTrackingState(true);
            timeTicker.SetTimersState(true);

            ShowMenu(MenuEnums.HUD_VIEW, true);
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

        public void ShowResultView()
        {
            inputController.SetInputLock(true);
            positionSystem.SetPositionTrackingState(false);
            resultView.SetResult(positionSystem.ToString(), TimeTicker.FormatLongTimeLeft(timeTicker.TickTimer));
            ShowMenu(MenuEnums.RESULT_VIEW, true);
            _playerController.OnRaceFinished -= ShowResultView;
        }

        public void LeaveGame()
        {
            _multiplayerManager.LeaveGame();
            ShowMenu(MenuEnums.HOME, true);
            _multiplayerManager = null;
        }
    }
}

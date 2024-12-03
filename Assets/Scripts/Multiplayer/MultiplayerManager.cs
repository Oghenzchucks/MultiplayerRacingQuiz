using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using MenuNavigation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Multiplayer
{
    public class MultiplayerManager : MonoBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField] private NetworkPrefabRef[] playerPrefabs;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private InputController inputController;

        private NetworkRunner _runner;
        public NetworkRunner GetRunner => _runner;

        private PlayerRef _playerRef;

        private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();
        public Dictionary<PlayerRef, NetworkObject> GetSpawnedCharacters => _spawnedCharacters;

        public bool ArePlayersComplete => _runner.SessionInfo.PlayerCount == _runner.SessionInfo.MaxPlayers;

        public async void StartGame(GameMode mode)
        {
            // Create the Fusion runner and let it know that we will be providing user input
            _runner = gameObject.AddComponent<NetworkRunner>();
            _runner.ProvideInput = true;

            // Create the NetworkSceneInfo from the current scene
            var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
            var sceneInfo = new NetworkSceneInfo();
            if (scene.IsValid)
            {
                sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
            }

            // Start or join (depends on gamemode) a session with a specific name
            await _runner.StartGame(new StartGameArgs()
            {
                GameMode = mode,
                SessionName = "TestRoom",
                Scene = scene,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
                PlayerCount = 2,
            });

            //inputController.SetInputLock(true);
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (player == _runner.LocalPlayer)
            {
                _playerRef = player;
            }

            if (!runner.IsServer)
            {
                return;
            }

            //CHECK GAME HAS NOT STARTED

            // SPAWN PLAYERS
            var playerCount = runner.SessionInfo.PlayerCount;
            NetworkObject networkPlayerObject = runner.Spawn(playerPrefabs[playerCount - 1], spawnPoint.position + new Vector3((spawnPoint.position.x + 10) * (1 - playerCount), 0, 0), Quaternion.identity, player);
            _spawnedCharacters.Add(player, networkPlayerObject);

            if (_spawnedCharacters.Count == _runner.SessionInfo.MaxPlayers)
            {
                //START THE GAME ON SERVER
                inputController.SetInputLock(false);
            }
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            Debug.Log("Player Left");
            if (runner.LocalPlayer == player)
            {
                MenuManager.OnLoadMenu?.Invoke(MenuEnums.MAIN_MENU, true);
            }

            if (!runner.IsServer)
            {
                return;
            }

            if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
            {
                runner.Despawn(networkObject);
                _spawnedCharacters.Remove(player);
            }
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            var data = new NetworkInputData();

            if (inputController.IsSendingInput)
            {
                data.direction = inputController.GetDriveDirection();
            }

            if (Input.GetKey(KeyCode.W))
                data.direction += new Vector3(0, 1, 0);

            if (Input.GetKey(KeyCode.S))
                data.direction += new Vector3(0, -1, 0);

            if (Input.GetKey(KeyCode.A))
                data.direction += Vector3.left;

            if (Input.GetKey(KeyCode.D))
                data.direction += Vector3.right;

            input.Set(data);
        }

        public void LeaveGame()
        {
            MenuManager.OnLoadMenu?.Invoke(MenuEnums.LOADING_VIEW, true);
            _runner.Disconnect(_playerRef);
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            MenuManager.OnLoadMenu?.Invoke(MenuEnums.MAIN_MENU, true);
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
        public void OnConnectedToServer(NetworkRunner runner) { }
        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
        public void OnSceneLoadDone(NetworkRunner runner) { }
        public void OnSceneLoadStart(NetworkRunner runner) { }
        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    }

}
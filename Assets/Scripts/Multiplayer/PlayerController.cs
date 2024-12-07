using System;
using Car;
using Fusion;
using MenuNavigation;
using UnityEngine;

namespace Multiplayer
{
    [RequireComponent(typeof(CarController))]
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private bool isSinglePlayer;
        private CarController _carController;

        [Networked, OnChangedRender(nameof(FinishedRace))]
        public bool HasFinishedRace { get; set; }
        public Action OnRaceFinished;

        private void Awake()
        {
            _carController = GetComponent<CarController>();
        }

        private void OnDestroy()
        {
            OnRaceFinished = null;
        }

        public override void Spawned()
        {
            if (HasInputAuthority)
            {
                MenuManager.OnLoadMenu?.Invoke(MenuEnums.HUD_VIEW, true);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (Runner.IsServer)
            {
                _carController.StopCar();
                HasFinishedRace = true;
            }

            if (isSinglePlayer)
            {
                FinishedRace();
            }
        }

        private void FinishedRace()
        {
            if (!HasInputAuthority)
            {
                return;
            }

            OnRaceFinished?.Invoke();
        }
    }
}
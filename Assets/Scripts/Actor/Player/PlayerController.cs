using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Evacuation.Session;
using Cinemachine;
using Evacuation.Cinematics;
using Evacuation.Model.Data;

public interface IPausable
{
    void OnPause();
    void OnUnpause();
}

public interface ICheckPaused
{
    bool CheckIsPaused();
}

public interface IPlayerInitialiser
{
    void InitialisePlayer(SceneController sceneController);
}

namespace Evacuation.Actor.PlayerSystems
{
    public class PlayerController : MonoBehaviour, IPlayerInitialiser, IPausable, ICheckPaused
    {
        private UnityEngine.InputSystem.PlayerInput playerInput;
        private bool isPaused = false;

        // ================================
        // Initialisers
        // ================================

        public void InitialisePlayer(SceneController sceneController)
        {
            // Initialises and sets primary data for vessel
            InitiateShipStatHandler();

            // Initialises and activates related handlers and controls
            AssignPlayerCameraToManager();
            InitiateInputSystem();
            InitiateMovement();
            InitiateWeapons();
            InitialiseShipRescueCabin();

            sceneController.OnGameCompletion.AddListener(RemoveInputSystems);
        }

        private void InitiateShipStatHandler()
        {
            ShipData shipData = SessionData.instance.selectedShip.GetShipData();

            print(shipData.FixedWeapons);

            IStatHandler statHandler = this.GetComponent<IStatHandler>();
            statHandler.InitialiseStats(SessionData.instance.selectedShip.GetShipData());
            PlayerHeathComponent heathComponent = this.GetComponent<PlayerHeathComponent>();
            heathComponent.InitialiseHealth(shipData.MaxHealth);
            PlayerShieldComponent shieldComponent = this.GetComponent<PlayerShieldComponent>();
            shieldComponent.InitialiseShield(shipData.MaxShield);
        }

        public void InitiateInputSystem()
        {
            playerInput = this.GetComponent<UnityEngine.InputSystem.PlayerInput>();

            if (Application.isMobilePlatform)
            {
                Debug.Log("Is ported to mobile");
                BeginMobileInputSystem();
            }
            else
            {
                Debug.Log("Is ported to desktop");
                BeginDesktopInputSystem();
            }
        }

        /// <summary>
        /// Initiates the mobile input systerm to this player
        /// </summary>
        private void BeginMobileInputSystem()
        {
            //Spawn UI HUD
            UISettings uiSettings = GameManager.Instance.uiSettings;
            GameObject mobileHUD = Instantiate(uiSettings.mobileUIHUDPrefab, Vector3.zero, Quaternion.identity) as GameObject;

            playerInput.SwitchCurrentActionMap("Mobile");
            DesktopInputManager desktopinput = this.GetComponent<DesktopInputManager>();
            desktopinput.enabled = false;

            IMobileInput mobileInput = this.GetComponent<IMobileInput>();
            mobileInput.InitialiseInput(mobileHUD);
        }


        /// <summary>
        /// Initiates the desktop input system for this player
        /// </summary>
        private void BeginDesktopInputSystem()
        {
            playerInput.SwitchCurrentActionMap("Desktop");
            MobileInputManager mobileInput = this.GetComponent<MobileInputManager>();
            mobileInput.enabled = false;

            IDesktopInput deskTopInput = this.GetComponent<IDesktopInput>();
            deskTopInput.InitialiseDesktop();
        }

        public void InitiateMovement()
        {
            PlayerMovementController playerMovement = this.GetComponent<PlayerMovementController>();
            playerMovement.InitialiseMovement();
        }

        public void InitiateWeapons()
        {
            PlayerWeaponController playerWeapons = this.GetComponent<PlayerWeaponController>();
            playerWeapons.InitialiseWeaponController();
            IDamageable damager = this.GetComponent<IDamageable>();
            damager.InitialiseComponent();
        }

        private void InitialiseShipRescueCabin()
        {
            PlayerRescueSystem rescueSystem = this.GetComponent<PlayerRescueSystem>();
            rescueSystem.InitialiseRescueSsytem();
        }

        private void AssignPlayerCameraToManager()
        {
            SceneController sceneController = GameManager.Instance.sceneController;
            CinematicManager cinematicManager = sceneController.CinematicManager;
            cinematicManager.PrimaryCamera = this.GetComponentInChildren<CinemachineVirtualCamera>();
        }

        // ================================
        // Pausers and Checkers
        // ================================

        public void OnPause()
        {
            isPaused = true;
        }

        public void OnUnpause()
        {
            isPaused = false;
        }

        public bool CheckIsPaused()
        {
            return isPaused;
        }

        // ================================
        // Game Completion
        // ================================

        /// <summary>
        /// Called to disable input control in the event of game events or game completion
        /// </summary>
        public void RemoveInputSystems()
        {
            Debug.LogWarning("Lockout Triggered");
            DesktopInputManager desktopinput = this.GetComponent<DesktopInputManager>();
            Destroy(desktopinput);

            MobileInputManager mobileInput = this.GetComponent<MobileInputManager>();
            Destroy(mobileInput);
        }
    }
}

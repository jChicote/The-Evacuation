using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    void InitialisePlayer();
}

namespace PlayerSystems
{
    public class PlayerController : MonoBehaviour, IPlayerInitialiser, IPausable, ICheckPaused
    {
        public UnityEngine.InputSystem.PlayerInput playerInput;
        public bool isPaused = false;

        // Start is called before the first frame update
        void Start()
        {
            InitialisePlayer();
        }

        /// <summary>
        /// Initialises classes systems to the player
        /// </summary>
        public void InitialisePlayer()
        {
            Debug.Log("Is initialising player");

            // Initialises and sets primary data for vessel
            InitiateShipStatHandler();

            // Initialises and activates related handlers and controls
            InitiateInputSystem();
            InitiateMovement();
            InitiateWeapons();
        }

        private void InitiateShipStatHandler()
        {
            IStatHandler statHandler = this.GetComponent<IStatHandler>();
            statHandler.InitialiseStats(SessionData.instance.selectedShip);
        }

        /// <summary>
        /// Intiailises player related input classes
        /// </summary>
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

        /// <summary>
        /// Initialises movement related classes.
        /// </summary>
        public void InitiateMovement()
        {
            PlayerMovementController playerMovement = this.GetComponent<PlayerMovementController>();
            playerMovement.InitialiseMovement();
        }

        public void InitiateWeapons()
        {
            PlayerWeaponController playerWeapons = this.GetComponent<PlayerWeaponController>();
            playerWeapons.InitialiseWeaponController();
        }

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
    }
}

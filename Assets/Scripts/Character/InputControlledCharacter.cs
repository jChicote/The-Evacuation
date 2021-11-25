using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TheEvacuation.InputSystem;

namespace TheEvacuation.Controllers
{
    public class InputControlledCharacter : BaseInteractiveObject
    {
        protected UnityEngine.InputSystem.PlayerInput playerInput;
        protected IMobileInput mobileInputController;
        protected IDesktopInputController desktopInputController;

        private void Start()
        {
            InitiateInputSystem();
        }

        public void InitiateInputSystem()
        {
            playerInput = this.GetComponent<UnityEngine.InputSystem.PlayerInput>();

            if (Application.isMobilePlatform)
            {
                print("Is ported to mobile");
                BeginMobileInputSystem();
            }
            else
            {
                print("Is ported to desktop");
                BeginDesktopInputSystem();
            }
        }

        /// <summary>
        /// Initiates the mobile input systerm to this player
        /// </summary>
        private void BeginMobileInputSystem()
        {
            //Spawn UI HUD
            //UISettings uiSettings = GameManager.Instance.uiSettings;
           // GameObject mobileHUD = Instantiate(uiSettings.mobileUIHUDPrefab, Vector3.zero, Quaternion.identity) as GameObject;

            playerInput.SwitchCurrentActionMap("Mobile");
            //DesktopInputManager desktopinput = this.GetComponent<DesktopInputManager>();
            // desktopinput.enabled = false;

            DisableDesktopInput(desktopInputController);

            IMobileInput mobileInput = this.GetComponent<IMobileInput>();
           // if (mobileInput != null)
                //mobileInput.InitialiseInput();
            //mobileInput.InitialiseInput(mobileHUD);
        }

        /// <summary>
        /// Initiates the desktop input system for this player
        /// </summary>
        private void BeginDesktopInputSystem()
        {
            playerInput.SwitchCurrentActionMap("Desktop");
            // MobileInputManager mobileInput = this.GetComponent<MobileInputManager>();
            // mobileInput.enabled = false;

            DisableMobileInput(mobileInputController);

            IDesktopInputController desktopInput = this.GetComponent<IDesktopInputController>();
            if (desktopInput != null)
                desktopInput.InitialiseDesktop();
        }

        private void DisableMobileInput(IMobileInput mobileInput)
        {
            if (mobileInput == null)
            {
                Debug.LogWarning("No Mobile Input has been found");
                return;
            }

            //mobileInput.enabled = false;
        }

        private void DisableDesktopInput(IDesktopInputController desktopInput)
        {
            if (desktopInput == null)
            {
                Debug.LogWarning("No Desktop Input has been found");
                return;
            }

            //desktopInput.enabled = false;
        }
    }

}
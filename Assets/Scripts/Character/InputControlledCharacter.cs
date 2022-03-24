using TheEvacuation.Player.Input;
using UnityEngine;

namespace TheEvacuation.Character
{
    public class InputControlledCharacter : CharacterController
    {
        protected UnityEngine.InputSystem.PlayerInput playerInput;
        //protected IMobileInput mobileInputController;
        protected IDesktopInputControlAdapter desktopInputController;

        private void Start()
        {
            InitiateInputSystem();
        }

        public void InitiateInputSystem()
        {
            playerInput = this.GetComponent<UnityEngine.InputSystem.PlayerInput>();

            if (Application.isMobilePlatform)
            {
                //print("Is ported to mobile");
                //BeginMobileInputSystem();
            }
            else
            {
                //print("Is ported to desktop")
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

            //IMobileInput mobileInput = this.GetComponent<IMobileInput>();
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

            //DisableMobileInput(mobileInputController);
            //print("Enabled Desktop input");

            IDesktopInputControlAdapter desktopInput = this.GetComponent<IDesktopInputControlAdapter>();
            if (desktopInput != null)
                desktopInput.InitialiseDesktop();
        }

        //private void DisableMobileInput(IMobileInput mobileInput)
        //{
        //    if (mobileInput == null)
        //    {
        //        Debug.LogWarning("No Mobile Input has been found");
        //        return;
        //    }

        //    //mobileInput.enabled = false;
        //}

        private void DisableDesktopInput(IDesktopInputControlAdapter desktopInput)
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
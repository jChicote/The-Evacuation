using TheEvacuation.Common;
using TheEvacuation.Infrastructure.GameSystems;
using TheEvacuation.Infrastructure.GameSystems.SceneSystems;
using TheEvacuation.PlayerSystems.Input;
using UnityEngine.InputSystem;

namespace TheEvacuation.Character
{

    public class PlayerInputSystemConfigurator : BaseInteractiveObject
    {

        #region - - - - - - Fields - - - - - -

        protected PlayerInput playerInput;
        //protected IMobileInput mobileInputController;
        protected IDesktopInputControlAdapter desktopInputController;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void InitiateInputSystem(IScenePauseEventHandler pauseEventHandler)
        {
            playerInput = GameManager.Instance.sceneLevelManager.inputSystemManager.playerInput;

            //if (Application.isMobilePlatform)
            //    BeginMobileInputSystem();
            //else
            BeginDesktopInputSystem(pauseEventHandler);
        }

        /// <summary>
        /// Initiates the mobile input systerm to this player
        /// </summary>
        //private void BeginMobileInputSystem()
        //{
        //    Spawn UI HUD
        //    UISettings uiSettings = GameManager.Instance.uiSettings;
        //    GameObject mobileHUD = Instantiate(uiSettings.mobileUIHUDPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        //    playerInput.SwitchCurrentActionMap("Mobile");
        //    DesktopInputManager desktopinput = this.GetComponent<DesktopInputManager>();
        //    desktopinput.enabled = false;

        //    DisableDesktopInput(desktopInputController);

        //    IMobileInput mobileInput = this.GetComponent<IMobileInput>();
        //    if (mobileInput != null)
        //        mobileInput.InitialiseInput();
        //    mobileInput.InitialiseInput(mobileHUD);
        //}

        /// <summary>
        /// Initiates the desktop input system for this player
        /// </summary>
        private void BeginDesktopInputSystem(IScenePauseEventHandler pauseEventHandler)
        {
            //playerInput.SwitchCurrentActionMap("Gameplay");
            // MobileInputManager mobileInput = this.GetComponent<MobileInputManager>();
            // mobileInput.enabled = false;

            //DisableMobileInput(mobileInputController);

            IDesktopInputControlAdapter desktopInput = this.gameObject.AddComponent<DesktopInputControlAdapter>();
            if (desktopInput != null)
                desktopInput.InitialiseDesktopInputControl(pauseEventHandler);

            playerInput.actions["Aim"].performed += desktopInput.OnAim;
            playerInput.actions["Attack"].performed += desktopInput.OnAttack;
            playerInput.actions["Movement"].performed += desktopInput.OnMovement;
            playerInput.actions["Pause"].performed += desktopInput.OnPause;

            playerInput.actions["Attack"].canceled += desktopInput.OnAttack;
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

        //private void DisableDesktopInput(IDesktopInputControlAdapter desktopInput)
        //{
        //    if (desktopInput == null)
        //    {
        //        Debug.LogWarning("No Desktop Input has been found");
        //        return;
        //    }

        //    //desktopInput.enabled = false;
        //}

        #endregion Methods

    }

}
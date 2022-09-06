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
        protected IDesktopInputControlAdapter desktopInputController;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void InitiateInputSystem(IScenePauseEventHandler pauseEventHandler)
        {
            playerInput = GameManager.Instance.sceneLevelManager.inputSystemManager.playerInput;
            BeginDesktopInputSystem(pauseEventHandler);
        }

        /// <summary>
        /// Initiates the desktop input system for this player
        /// </summary>
        private void BeginDesktopInputSystem(IScenePauseEventHandler pauseEventHandler)
        {
            IDesktopInputControlAdapter desktopInput = this.gameObject.AddComponent<DesktopInputControlAdapter>();
            if (desktopInput != null)
                desktopInput.InitialiseDesktopInputControl(pauseEventHandler);

            playerInput.actions["Aim"].performed += desktopInput.OnAim;
            playerInput.actions["Attack"].performed += desktopInput.OnAttack;
            playerInput.actions["Movement"].performed += desktopInput.OnMovement;
            playerInput.actions["Pause"].performed += desktopInput.OnPause;
            playerInput.actions["Attack"].canceled += desktopInput.OnAttack;
        }

        #endregion Methods

    }

}
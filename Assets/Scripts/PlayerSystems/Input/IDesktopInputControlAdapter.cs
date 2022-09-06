using TheEvacuation.Infrastructure.GameSystems.SceneSystems;
using UnityEngine.InputSystem;

namespace TheEvacuation.PlayerSystems.Input
{

    public interface IDesktopInputControlAdapter : IPlayerInputEnabler
    {

        #region - - - - - - Methods - - - - - -

        void InitialiseDesktopInputControl(IScenePauseEventHandler pauseEventHandler);

        void OnAim(InputAction.CallbackContext value);

        void OnAttack(InputAction.CallbackContext value);

        void OnMovement(InputAction.CallbackContext value);

        void OnPause(InputAction.CallbackContext value);

        #endregion Methods

    }

}

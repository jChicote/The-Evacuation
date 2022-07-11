using TheEvacuation.Common;
using TheEvacuation.Infrastructure.GameSystems.SceneSystems;
using TheEvacuation.PlayerSystems.Movement;
using TheEvacuation.PlayerSystems.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheEvacuation.PlayerSystems.Input
{

    public interface IInputToggling
    {

        #region - - - - - - Methods - - - - - -

        void ToggleInputActivation(bool enabled);

        #endregion Methods

    }

    public interface IDesktopInputControlAdapter : IInputToggling
    {

        #region - - - - - - Methods - - - - - -

        void InitialiseDesktopInputControl(IScenePauseEventHandler pauseEventHandler);

        #endregion Methods

    }

    public class DesktopInputControlAdapter : MonoBehaviour, IDesktopInputControlAdapter, IPausable
    {

        #region - - - - - - Fields - - - - - -

        private ICharacterMovement possessedCharacterMovement;
        private ICharacterWeaponSystem possessedCharacterWeaponSystem;
        private IInputWeaponSystemVariables possessedWSInputVariables;
        private IScenePauseEventHandler pauseEventHandler;

        public bool inputActive;

        private Vector2 centerPosition;
        private Vector2 currentMousePosition;
        private Vector3 currentKeyMovementNormals;

        public bool IsPaused { get; set; } = false;

        #endregion Fields

        #region - - - - - - Methods - - - - - -
        public void InitialiseDesktopInputControl(IScenePauseEventHandler pauseEventHandler)
        {
            this.pauseEventHandler = pauseEventHandler;

            possessedCharacterMovement = this.GetComponent<ICharacterMovement>();
            possessedCharacterWeaponSystem = this.GetComponent<ICharacterWeaponSystem>();
            possessedWSInputVariables = this.GetComponent<IInputWeaponSystemVariables>();

            inputActive = false;
            centerPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        }

        private void DirectWeaponRotatorsToPoint(InputValue value)
        {
            // Seperate functionality and does not attach itself to any event action.

            /*for (int i = 0; i < playerWeapons.GetWeaponRotators().Length; i++)
            {
                playerWeapons.GetWeaponRotators()[i].ProvidePointerLocation(Camera.main.ScreenToWorldPoint(value.Get<Vector2>()));
            }*/
        }

        private void OnAim(InputValue value)
        {
            //if (!inputActive || IsPaused) return;
            if (!inputActive) return;

            currentMousePosition = value.Get<Vector2>();
            possessedCharacterMovement.CalculateShipRotation(centerPosition, currentMousePosition);

            DirectWeaponRotatorsToPoint(value);
        }

        private void OnAttack(InputValue value)
        {
            //if (!inputActive || IsPaused) return;
            if (!inputActive) return;

            possessedCharacterWeaponSystem.IsFiring = value.isPressed;
            possessedWSInputVariables.InputMousePosition = currentMousePosition;
        }

        private void OnDetach(InputValue value)
        {
            //shipDetacher.DetachFromPlatform();
        }

        private void OnMovement(InputValue value)
        {
            //if (!inputActive || IsPaused) return;
            if (!inputActive) return;

            currentKeyMovementNormals = value.Get<Vector2>();
            possessedCharacterMovement.IsMovementHeld = value.Get<Vector2>() != Vector2.zero;
            possessedCharacterMovement.CalculateMovement(currentKeyMovementNormals);
        }

        private void OnPause(InputValue value)
            => pauseEventHandler.ToggleGamePause();

        public void OnPauseEntity()
            => IsPaused = true;

        public void OnUnpauseEntity()
            => IsPaused = false;

        public void ToggleInputActivation(bool enabled)
            => inputActive = enabled;

        #endregion Methods

    }

}

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
        void ToggleInputActivation(bool enabled);
    }

    public interface IDesktopInputControlAdapter : IInputToggling
    {
        void InitialiseDesktopInputControl(IScenePauseEventHandler pauseEventHandler);
    }

    public class DesktopInputControlAdapter : MonoBehaviour, IDesktopInputControlAdapter
    {

        #region - - - - - - Fields - - - - - -

        private ICharacterMovement possessedCharacterMovement;
        private ICharacterWeaponSystem possessedCharacterWeaponSystem;
        private IInputWeaponSystemVariables possessedWSInputVariables;

        private IPausable pauseInstance;
        private IScenePauseEventHandler pauseEventHandler;

        public bool inputActive;

        private Vector2 centerPosition;
        private Vector2 currentMousePosition;
        private Vector3 currentKeyMovementNormals;

        #endregion Fields

        #region - - - - - - Methods - - - - - -
        public void InitialiseDesktopInputControl(IScenePauseEventHandler pauseEventHandler)
        {
            pauseInstance = this.GetComponent<IPausable>();
            this.pauseEventHandler = pauseEventHandler;
            possessedCharacterMovement = this.GetComponent<ICharacterMovement>();
            possessedCharacterWeaponSystem = this.GetComponent<ICharacterWeaponSystem>();
            possessedWSInputVariables = this.GetComponent<IInputWeaponSystemVariables>();

            inputActive = false;

            centerPosition = new Vector2();
            centerPosition.x = Screen.width / 2;
            centerPosition.y = Screen.height / 2;
        }

        public void ToggleInputActivation(bool enabled)
        {
            inputActive = enabled;
        }

        // Summary:
        // Provides movement vectors based on 2-Dimensional keyboard movement normalised before processing.
        // Movement vectors provided are the net directions relative to a fixed center.
        private void OnMovement(InputValue value)
        {
            if (!inputActive) return;
            if (pauseInstance.IsPaused) return;

            currentKeyMovementNormals = value.Get<Vector2>();
            possessedCharacterMovement.IsMovementHeld = value.Get<Vector2>() != Vector2.zero;
            possessedCharacterMovement.CalculateMovement(currentKeyMovementNormals);

            print(currentKeyMovementNormals);
        }

        // Summary:
        // Stores the aim position of the present position of the mouse within the game view.
        private void OnAim(InputValue value)
        {
            if (!inputActive) return;

            currentMousePosition = value.Get<Vector2>();
            possessedCharacterMovement.CalculateShipRotation(centerPosition, currentMousePosition);

            DirectWeaponRotatorsToPoint(value);
        }

        /// <summary>
        /// Called to provide pointer locations of input to rotators.
        /// </summary>
        private void DirectWeaponRotatorsToPoint(InputValue value)
        {
            // Seperate functionality and does not attach itself to any event action.

            /*for (int i = 0; i < playerWeapons.GetWeaponRotators().Length; i++)
            {
                playerWeapons.GetWeaponRotators()[i].ProvidePointerLocation(Camera.main.ScreenToWorldPoint(value.Get<Vector2>()));
            }*/
        }

        private void OnPause(InputValue value)
        {
            pauseEventHandler.ToggleGamePause();
        }

        private void OnAttack(InputValue value)
        {
            if (!inputActive) return;

            possessedCharacterWeaponSystem.IsFiring = value.isPressed;
            possessedWSInputVariables.InputMousePosition = currentMousePosition;
        }

        private void OnDetach(InputValue value)
        {
            //shipDetacher.DetachFromPlatform();
        }

        #endregion Methods

    }

}

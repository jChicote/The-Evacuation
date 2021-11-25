using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TheEvacuation;

namespace TheEvacuation.InputSystem
{
    public interface IInputToggling
    {
        void ToggleInputActivation(bool enabled);
    }

    public interface IDesktopInputController: IInputToggling
    {
        void InitialiseDesktop();
    }

    public class DesktopInputController : MonoBehaviour, IDesktopInputController
    {
        //private ICharacterMovement possessedCharacterMovement;
       // private ICharacterWeaponController possessedCharacterWeaponController;
        private IPausable pauseInstance;

        private Vector2 centerPosition;
        private Vector2 currentMousePosition;
        private Vector3 currentKeyMovementNormals;

        public void InitialiseDesktop()
        {
            pauseInstance = this.GetComponent<IPausable>();
        }

        public void ToggleInputActivation(bool enabled)
        {
            throw new System.NotImplementedException();
        }

        // Summary:
        // Provides movement vectors based on 2-Dimensional keyboard movement normalised before processing.
        // Movement vectors provided are the net directions relative to a fixed center.
        private void OnMovement(InputValue value)
        {
            if (pauseInstance.IsPaused) return;

            currentKeyMovementNormals = value.Get<Vector2>();
            //playerMovement.SetTriggerIsHeld(value.Get<Vector2>() != Vector2.zero);
            //currentMousePosition = value.Get<Vector2>();
            // playerMovement.CalculateMovement(centerPosition, currentMousePosition);
        }


        // Summary:
        // Stores the aim position of the present position of the mouse within the game view.
        private void OnAim(InputValue value)
        {
            currentMousePosition = value.Get<Vector2>();
            //playerMovement.CalculateLocalRotation(centerPosition, currentAimPosition);

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
            PauseScreen pauseMenu = GameManager.Instance.sceneController.pauseMenu;

            if (pauseMenu == null) return;
            if (pauseInstance.IsPaused)
            {
                pauseMenu.OnPause();
            }
            else
            {
                pauseMenu.OnResume();
            }
        }

        private void OnAttack(InputValue value)
        {
            //Debug.Log(value.isPressed);
            //playerWeapons.ActivateWeapons(value.isPressed);
        }

        private void OnDetach(InputValue value)
        {
            //shipDetacher.DetachFromPlatform();
        }
    }
}

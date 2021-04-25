using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Evacuation.Actor.PlayerSystems;
using Evacuation.Weapons;

public interface IDesktopInput
{
    void InitialiseDesktop();
}

public class DesktopInputManager : MonoBehaviour, IDesktopInput
{
    private ICheckPaused pauseChecker;
    private IMovement playerMovement;
    private IWeaponController playerWeapons;
    private IShipToPlatformAction shipDetacher;

    private Vector2 centerPosition;
    private Vector2 currentMousePosition;
    private Vector2 currentAimPosition;
    
    public void InitialiseDesktop()
    {
        playerMovement = this.GetComponent<IMovement>();
        playerWeapons = this.GetComponent<IWeaponController>();
        pauseChecker = this.GetComponent<ICheckPaused>();
        shipDetacher = this.GetComponent<IShipToPlatformAction>();
        centerPosition = new Vector2();
        centerPosition.x = Screen.width / 2;
        centerPosition.y = Screen.height / 2;
    }

    // Summary:
    // Provides movement vectors based on 2-Dimensional keyboard movement normalised before processing.
    // Movement vectors provided are the net directions relative to a fixed center.
    private void OnMovement(InputValue value)
    {
        if (pauseChecker.CheckIsPaused()) return;

        playerMovement.SetTriggerIsHeld(value.Get<Vector2>() != Vector2.zero);
        currentMousePosition = value.Get<Vector2>();
        playerMovement.CalculateMovement(centerPosition, currentMousePosition);
    }


    // Summary:
    // Stores the aim position of the present position of the mouse within the game view.
    private void OnAim(InputValue value)
    {
        currentAimPosition = value.Get<Vector2>();
        playerMovement.CalculateLocalRotation(centerPosition, currentAimPosition);

        DirectWeaponRotatorsToPoint(value);
    }

    /// <summary>
    /// Called to provide pointer locations of input to rotators.
    /// </summary>
    private void DirectWeaponRotatorsToPoint(InputValue value)
    {
        // Seperate functionality and does not attach itself to any event action.

        for (int i = 0; i < playerWeapons.GetWeaponRotators().Length; i++)
        {
            playerWeapons.GetWeaponRotators()[i].ProvidePointerLocation(value.Get<Vector2>());
        }
    }

    private void OnPause(InputValue value)
    {
        PauseScreen pauseMenu = GameManager.Instance.sceneController.pauseMenu;

        if (pauseMenu == null) return;
        if (pauseChecker.CheckIsPaused())
        {
            pauseMenu.OnPause();
        } else
        {
            pauseMenu.OnResume();
        }
    }

    private void OnAttack(InputValue value)
    {
        //Debug.Log(value.isPressed);
        playerWeapons.ActivateWeapons(value.isPressed);
    }

    private void OnDetach(InputValue value)
    {
        shipDetacher.DetachFromPlatform();
    }

    // Needed for presenting GUI options for disabling
    //
    private void OnGUI() { }
}

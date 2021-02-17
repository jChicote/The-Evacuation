using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IDesktopInput
{
    void InitialiseDesktop();
}

public class DesktopInputManager : MonoBehaviour, IDesktopInput
{
    private ICheckPaused pauseChecker;
    private IMovement playerMovement;
    private IWeaponController playerWeapons;

    private Vector2 centerPosition;
    private Vector2 currentMousePosition;
    
    public void InitialiseDesktop()
    {
        playerMovement = this.GetComponent<IMovement>();
        playerWeapons = this.GetComponent<IWeaponController>();
        pauseChecker = this.GetComponent<ICheckPaused>();
        centerPosition = new Vector2();
        centerPosition.x = Screen.width / 2;
        centerPosition.y = Screen.height / 2;
    }

    private void OnMovement(InputValue value)
    {
        if (pauseChecker.CheckIsPaused()) return;

        currentMousePosition = value.Get<Vector2>();
        playerMovement.CalculateMovement(centerPosition, currentMousePosition);
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
        Debug.Log(value.isPressed);
        playerWeapons.ActivateWeapons(value.isPressed);
    }
}

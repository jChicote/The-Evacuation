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
    private IMovement playerMovement;
    private Vector2 centerPosition;
    private Vector2 currentMousePosition;
    
    public void InitialiseDesktop()
    {
        playerMovement = this.GetComponent<IMovement>();
        centerPosition = new Vector2();
        centerPosition.x = Screen.width / 2;
        centerPosition.y = Screen.height / 2;
    }

    private void OnMovement(InputValue value)
    {
        currentMousePosition = value.Get<Vector2>();

        playerMovement.CalculateMovement(centerPosition, currentMousePosition);
    }
}

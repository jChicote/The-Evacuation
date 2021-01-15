using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.EnhancedTouch;

public interface IMobileInput
{
    void InitialiseInput(GameObject mobileHUD);
}

public class MobileInputManager : MonoBehaviour, IMobileInput
{
    //  Interfaces
    private ITouchLeftNavpadControl joystick;
    private ICheckPaused pauseChecker;
    private IMovement playerMovement;

    //  Data Variables
    private ReadOnlyArray<UnityEngine.InputSystem.EnhancedTouch.Touch> activeTouches;
    private Vector2 startTouchPosition;
    private Vector2 touchPosition;

    private int currentTocuhID;
    private bool isJoystickActive = false;

    /// <summary>
    /// Initialises the input system for the input manager.
    /// </summary>
    public void InitialiseInput(GameObject mobileHUD)
    {
        joystick = mobileHUD.GetComponent<ITouchLeftNavpadControl>();
        pauseChecker = this.GetComponent<ICheckPaused>();
        playerMovement = this.GetComponent<IMovement>();
        Debug.Log("joystick enabled");
    }

    /// <summary>
    /// Runs on enable of the class this gameobject is attached to.
    /// </summary>
    protected void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
    }

    /// <summary>
    /// Disables when invoked or closed.
    /// </summary>
    protected void OnDisable()
    {
        EnhancedTouchSupport.Disable();
        TouchSimulation.Disable();
    }

    /// <summary>
    /// Performs an update during each frame
    /// </summary>
    private void Update()
    {
        ProcessTouchInput();
    }

    /// <summary>
    /// Processes all touch input coming through the input system.
    /// </summary>
    private void ProcessTouchInput()
    {
        if (pauseChecker.CheckIsPaused())
        {
            activeTouches = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;

            if (activeTouches.Count > 0)
            {
                for (var i = 0; i < activeTouches.Count; i++)
                {
                    OnJoyStickControl(activeTouches[i]);
                }
            }
        } else
        {
            joystick.HidePad();
        }
        
    }

    /// <summary>
    /// Dictates the control behaviour of the Joystick UI.
    /// </summary>
    private void OnJoyStickControl(UnityEngine.InputSystem.EnhancedTouch.Touch touch)
    {
        touchPosition = touch.screenPosition;
        startTouchPosition = touch.startScreenPosition;

        if (touchPosition.x > Screen.width) return;
        if (!isJoystickActive)
        {
            currentTocuhID = touch.touchId;
            isJoystickActive = true;
        } 

        if (currentTocuhID == touch.touchId && isJoystickActive)
        {
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                joystick.RevealPad(startTouchPosition);
            }

            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                joystick.TransformNavStick(touchPosition);
            }

            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
            {
                joystick.HidePad();
            }

            playerMovement.CalculateMovement(startTouchPosition, touchPosition);
        }
    }
}

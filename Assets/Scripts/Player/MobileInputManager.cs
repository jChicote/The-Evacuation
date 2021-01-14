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
    /*public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;*/


    private ITouchLeftNavpadControl joystick;
    private ReadOnlyArray<UnityEngine.InputSystem.EnhancedTouch.Touch> activeTouches;
    private Vector2 touchPosition;

    /// <summary>
    /// 
    /// </summary>
    public void InitialiseInput(GameObject mobileHUD)
    {
        joystick = mobileHUD.GetComponent<ITouchLeftNavpadControl>();
        Debug.Log("joystick enabled");

    }

    protected void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
    }

    protected void OnDisable()
    {
        EnhancedTouchSupport.Disable();
        TouchSimulation.Disable();
    }


    /// <summary>
    /// 
    /// </summary>
    private void OnMovement(InputValue value)
    {
        //lastTouchPosition = value.Get<Vector2>();
        //Debug.Log(touch.phase);
        //Debug.Log("Hold position at: " + touch.position);
        //Debug.Log(value.Get<TouchState>());
        //touch = value.Get<TouchState>();
        //Debug.Log(touch.ToString());


        //Determine whether movement topuch detection is on the correct screen side
        /*if (touch.position.x <= Screen.width / 2)
        {

            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                leftNavpad.RevealPad(touch.position);
            }

            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                leftNavpad.TransformNavStick(touch.position);
            }

            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
            {
                leftNavpad.HidePad();
            }
        }*/
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        ProcessTouchInput();
    }

    /// <summary>
    /// 
    /// </summary>
    private void ProcessTouchInput()
    {
        activeTouches = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;

        if (activeTouches.Count > 0)
        {
            for (var i = 0; i < activeTouches.Count; i++)
            {
                //Debug.Log("Active touch: " + activeTouches[i]);
                //Debug.Log("Position at: " + activeTouches[i].screenPosition);
                OnJoyStickControl(activeTouches[i]);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnJoyStickControl(UnityEngine.InputSystem.EnhancedTouch.Touch touch)
    {
        touchPosition = touch.screenPosition;

        if (touchPosition.x <= Screen.width / 2)
        {
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                joystick.RevealPad(touchPosition);
            }

            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                joystick.TransformNavStick(touchPosition);
            }

            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
            {
                joystick.HidePad();
            }
        }
    }


}

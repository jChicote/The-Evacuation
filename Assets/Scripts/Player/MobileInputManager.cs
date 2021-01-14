using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Controls;

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


    private ITouchLeftNavpadControl leftNavpad;
    //private TouchState touch;
    private TouchControl touchControl;


    private Vector2 lastTouchPosition;

    /// <summary>
    /// 
    /// </summary>
    public void InitialiseInput(GameObject mobileHUD)
    {
        leftNavpad = mobileHUD.GetComponent<ITouchLeftNavpadControl>();
        touchControl = new TouchControl();
    }

    protected void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();

        //UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    protected void OnDisable()
    {
        EnhancedTouchSupport.Disable();
        TouchSimulation.Disable();

        //UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
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

    /*private void FingerDown(Finger finger)
    {
        if (OnStartTouch != null) OnEndTouch(finger.screenPosition, Time.time);
    }

    private void FingerUp(Finger finger)
    {
        if (OnStartTouch != null) OnStartTouch(finger.screenPosition, Time.time);
    }*/

    private void Update()
    {
        var activeTouches = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;
        for (var i = 0; i < activeTouches.Count; i++)
        {
            Debug.Log("Active touch: " + activeTouches[i]);
            Debug.Log("Position at: " + activeTouches[i].screenPosition);
        }
    }

}

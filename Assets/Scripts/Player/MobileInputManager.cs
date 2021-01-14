using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public interface IMobileInput
{
    void InitialiseInput(GameObject mobileHUD);
}

public class MobileInputManager : MonoBehaviour, IMobileInput
{
    private ITouchLeftNavpadControl leftNavpad;
    private TouchState touch;

    private Vector2 lastTouchPosition;

    /// <summary>
    /// 
    /// </summary>
    public void InitialiseInput(GameObject mobileHUD)
    {
        leftNavpad = mobileHUD.GetComponent<ITouchLeftNavpadControl>();
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
        touch = value.Get<TouchState>();
        Debug.Log(touch.ToString());

        //Determine whether movement topuch detection is on the correct screen side
        if (touch.position.x <= Screen.width / 2)
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
        }
    }

}

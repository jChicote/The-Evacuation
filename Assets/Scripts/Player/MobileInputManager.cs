using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IMobileInput
{
    void InitialiseInput(GameObject mobileHUD);
}

public class MobileInputManager : MonoBehaviour, IMobileInput
{
    private ITouchLeftNavpadControl leftNavpad;

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
        lastTouchPosition = value.Get<Vector2>();
        Debug.Log(value.isPressed);
        Debug.Log("Hold position at: " + value.Get<Vector2>());

        //Determine whether movement topuch detection is on the correct screen side
        /*if (lastTouchPosition.x <= Screen.width / 2)
        {
            if (value.isPressed)
            {
                Debug.Log("Hold position at: " + value.Get<Vector2>());
                leftNavpad.RevealPad(lastTouchPosition);
                leftNavpad.TransformNavStick(lastTouchPosition);
            }

            if (!value.isPressed)
            {
                leftNavpad.HidePad();
            }
        }*/
    }

}

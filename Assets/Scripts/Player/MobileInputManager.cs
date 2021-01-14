using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public interface IMobileInput
{
    void InitialiseInput();
}

public class MobileInputManager : MonoBehaviour, IMobileInput
{



    /// <summary>
    /// 
    /// </summary>
    public void InitialiseInput()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    private void OnMovement(TouchState value)
    {
        Debug.Log("Value Passed");
        Debug.Log("Hold position at: " + value.position);
    }

}

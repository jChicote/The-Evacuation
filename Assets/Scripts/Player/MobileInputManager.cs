using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private void OnMovement(Vector2 value)
    {
        Debug.Log("Value Passed");
        Debug.Log("Hold position at: " + value);
    }

}

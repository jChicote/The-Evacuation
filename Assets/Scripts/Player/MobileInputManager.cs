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

    private void OnMovement(InputValue value)
    {
        Debug.Log("Value");
    }

}

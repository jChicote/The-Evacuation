using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInitialiser
{
    void InitialisePlayer();
}

public class PlayerController : MonoBehaviour, IPlayerInitialiser
{
    // Start is called before the first frame update
    void Start()
    {
        InitialisePlayer();
    }

    /// <summary>
    /// 
    /// </summary>
    public void InitialisePlayer()
    {
        Debug.Log("Is initialising player");
        InitiateInputSystem();
    }

    /// <summary>
    /// 
    /// </summary>
    public void InitiateInputSystem()
    {
        if (Application.isMobilePlatform)
        {
            Debug.Log("Is ported to mobile");
            IMobileInput mobileInput = this.GetComponent<IMobileInput>();
        }
    }
}
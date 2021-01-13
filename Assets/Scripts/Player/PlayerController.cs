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
        InitiateInputSystem();
    }

    /// <summary>
    /// 
    /// </summary>
    public void InitiateInputSystem()
    {
        if (Application.isMobilePlatform)
        {
            IMobileInput mobileInput = this.GetComponent<IMobileInput>();
        }
    }
}

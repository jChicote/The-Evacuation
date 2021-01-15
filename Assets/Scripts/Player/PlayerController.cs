using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPausable
{
    void OnPause();
    void OnUnpause();
}

public interface ICheckPaused
{
    bool CheckIsPaused();
}

public interface IPlayerInitialiser
{
    void InitialisePlayer();
}

public class PlayerController : MonoBehaviour, IPlayerInitialiser, IPausable, ICheckPaused
{
    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        InitialisePlayer();
    }

    /// <summary>
    /// Initialises classes systems to the player
    /// </summary>
    public void InitialisePlayer()
    {
        Debug.Log("Is initialising player");
        InitiateInputSystem();
        InitiateMovement();
    }

    /// <summary>
    /// Intiailises player related input classes
    /// </summary>
    public void InitiateInputSystem()
    {
        if (Application.isMobilePlatform)
        {
            Debug.Log("Is ported to mobile");

            //Spawn UI HUD
            UISettings uiSettings = GameManager.Instance.uiSettings;
            GameObject mobileHUD = Instantiate(uiSettings.mobileUIHUDPrefab, Vector3.zero, Quaternion.identity) as GameObject;


            IMobileInput mobileInput = this.GetComponent<IMobileInput>();
            mobileInput.InitialiseInput(mobileHUD);
        } else
        {
            Debug.Log("Is ported to desktop");
            //MobileInputManager mobileInput = this.GetComponent<MobileInputManager>();
            //mobileInput.enabled = false;

        }
    }

    public void InitiateMovement()
    {
        PlayerMovementController playerMovement = this.GetComponent<PlayerMovementController>();
        playerMovement.InitialiseMovement();
    }

    public void OnPause()
    {
        isPaused = true;
    }

    public void OnUnpause()
    {
        isPaused = false;
    }

    public bool CheckIsPaused()
    {
        return isPaused;
    }
}

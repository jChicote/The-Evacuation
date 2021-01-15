using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [Header("Visual Elements")]
    public GameObject pausePanel;
    public Image pauseBackImage;

    private bool isRevealed = false;

    /// <summary>
    /// 
    /// </summary>
    public void RevealPauseScreen(bool isActive)
    {
        Debug.Log("is triggered");
        isRevealed = isActive;
        pausePanel.SetActive(isRevealed);
        pauseBackImage.enabled = isRevealed;
    }

    /// <summary>
    /// Engages pause on all objects with the interface.
    /// </summary>
    public void OnPause()
    {
        IPausable pausableItem;

        GameObject[] pausableObjects = FindObjectsOfType<GameObject>();
        
        foreach(GameObject item in pausableObjects)
        {
            pausableItem = item.GetComponent<IPausable>();

            if(pausableItem != null)
            {
                Debug.Log("had ipausable");
                Debug.Log(pausableItem.ToString());
                pausableItem.OnPause();
            }
        }

        RevealPauseScreen(true);
    }

    /// <summary>
    /// Disengages pause from all objects with the interface.
    /// </summary>
    public void OnResume()
    {
        Debug.Log("Resumed");

        IPausable pausableItem;

        GameObject[] pausableObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject item in pausableObjects)
        {
            pausableItem = item.GetComponent<IPausable>();

            if (pausableItem != null)
            {
                pausableItem.OnUnpause();
            }
        }

        RevealPauseScreen(false);
    }

    public void OnRestart()
    {
        Debug.Log("Restarted");
    }

    public void OnSettings()
    {
        Debug.Log("Open Settings");
    }

    public void OnQuit()
    {
        GameManager.Instance.sceneLoader.LoadMainMenu();
    }
}

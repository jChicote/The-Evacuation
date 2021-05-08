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
        IPausable[] pausableItems;

        GameObject[] pausableObjects = FindObjectsOfType<GameObject>();
        
        foreach(GameObject item in pausableObjects)
        {
            pausableItems = item.GetComponents<IPausable>();

            foreach (IPausable pausible in pausableItems)
            {
                if (pausible != null)
                {
                    pausible.OnPause();
                }
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

        IPausable[] pausableItems;

        GameObject[] pausableObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject item in pausableObjects)
        {
            pausableItems = item.GetComponents<IPausable>();

            foreach (IPausable pausible in pausableItems)
            {
                if (pausible != null)
                {
                    pausible.OnUnpause();
                }
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

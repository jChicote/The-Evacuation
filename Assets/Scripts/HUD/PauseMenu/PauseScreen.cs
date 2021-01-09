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

    // Start is called before the first frame update

    public void RevealPauseScreen()
    {
        Debug.Log("is triggered");
        isRevealed = !isRevealed;
        pausePanel.SetActive(isRevealed);
        pauseBackImage.enabled = isRevealed;
    }

    public void OnResume()
    {
        Debug.Log("Resumed");

        RevealPauseScreen();
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

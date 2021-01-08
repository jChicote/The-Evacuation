using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    [Header("Visual Elements")]
    public GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnResume()
    {
        Debug.Log("Resumed");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ILoadingScreen
{
    void DisplayLoadingScreen();
    void HideLoadingScreen();
    void UpdateLoadingScreen(float progressValue);
}

public class LoadingScreen : MonoBehaviour, ILoadingScreen
{
    public static LoadingScreen Instance = null;

    public Slider loadingBar;
    public Image screenImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Updates the loading screen UI at runtime when called.
    /// </summary>
    public void UpdateLoadingScreen(float progressValue)
    {
        RevealImageSlides();
        UpdateProgressBar(progressValue);
    }

    /// <summary>
    /// Displays the loading screen before progressing to next screen.
    /// </summary>
    public void DisplayLoadingScreen()
    {
        loadingBar.gameObject.SetActive(true);
        screenImage.gameObject.SetActive(true);

        loadingBar.value = 0;
        screenImage.color = Color.black;
    }

    /// <summary>
    /// Animates between different background images. NOT YET ADDED
    /// </summary>
    private void RevealImageSlides()
    {
        if (screenImage == null) return;
        screenImage.color = Color.black;
    }

    /// <summary>
    /// Updates progress value on UI Slider.
    /// </summary>
    private void UpdateProgressBar(float progressValue)
    {
        if (loadingBar == null) return;
        loadingBar.value = progressValue;
    }

    /// <summary>
    /// Hides loading screen after successful loading.
    /// </summary>
    public void HideLoadingScreen()
    {
        loadingBar.value = 0;
        screenImage.color = Color.black;

        loadingBar.gameObject.SetActive(false);
        screenImage.gameObject.SetActive(false);
        Debug.Log("Triggered hidden");
    }
}

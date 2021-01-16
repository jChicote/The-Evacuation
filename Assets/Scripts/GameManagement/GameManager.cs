using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [Header("Settings")]
    public UISettings uiSettings;
    public WeaponSettings weaponSettings;

    [Header("Player Events")]
    public UnityEvent OnMainMenu;

    [Header("Loading Screen UI")]
    public GameObject LoadingScreen;

    [HideInInspector] public MainMenu mainMenuUI = null;
    [HideInInspector] public SceneController sceneController;
    public SceneLoader sceneLoader;

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

        sceneLoader.Init();
        OnMainMenu.AddListener(ShowMainMenu);

        //Create scene loader and initialise.
        //sceneLoader = this.GetComponent<SceneLoader>();
    }

    private void Start()
    {
        ShowMainMenu();
    }

    /// <summary>
    /// Reveals menu when called.
    /// </summary>
    public void ShowMainMenu()
    {
        if (mainMenuUI != null) return;

        mainMenuUI = Instantiate(uiSettings.mainMenuPrefab, transform.position, Quaternion.identity).GetComponent<MainMenu>();
    }

    /// <summary>
    /// Destroys menu when called.
    /// </summary>
    public void CloseMainMenu()
    {
        mainMenuUI.OnClose();
    }
}

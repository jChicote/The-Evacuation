using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using UserInterface.HUD;
using PlayerSystems;

public class SceneController : MonoBehaviour
{
    // Public Events
    public UnityEvent OnGameplayStart;
    public UnityEvent OnPlayerRespawn;
    public UnityEvent OnPlayerDeath;
    public UnityEvent OnGameCompletion;

    [Header("User Interfaces")]
    public PauseScreen pauseMenu;
    public PlayerHUDManager playerHUD;
    [SerializeField] private GameCompletionHUD completionHUD;

    [Header("Scene Managers & Systems")]
    public ScoreSystem scoreSystem;

    [Space]
    [SerializeField] private LevelData levelData;

    // ================================
    // Setters and Initialisers
    // ================================

    // Start is called before the first frame update
    private void Awake()
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.sceneController = this;

        OnGameCompletion = new UnityEvent();
        OnGameCompletion.AddListener(RevealGameCompletionHUD);

        LoadLevelData();
        PrepareScene();
    }

    private void LoadLevelData()
    {
        // Currently we are loading directly from defaults 
        // In the future this should be transferred to a session data setup

        levelData = GameManager.Instance.levelSettings.defaultLevelData.Where(x => x.levelID == "#00091").First();
    }

    // ================================
    // UI and Systems
    // ================================

    /// <summary>
    /// Manages the sequential creation of important scene objects.
    /// </summary>
    private void PrepareScene()
    {
        //LoadSceneCamera();
        LoadHUD();

        //Load Entities
        SpawnPlayer();
        SpawnEnemyEntities();
    }

    /// <summary>
    /// Loads game HUD UI.
    /// </summary>
    private void LoadHUD()
    {
        scoreSystem.InitialiseScoreSystem(levelData.ConvertToScoreData());
        playerHUD.InitialiseHud(scoreSystem.GetComponent<IScoreEventAssigner>(), levelData, this);
        scoreSystem.IncrementScoreAmount(100);
        scoreSystem.ForceLabelUpdate();

    }

    public void RevealGameCompletionHUD()
    {
        completionHUD.gameObject.SetActive(true);
        completionHUD.InitialiseGameCompletionHUD(levelData);
    }

    // ================================
    // Entity Loaders
    // ================================

    /// <summary>
    /// Spawns the selected player into the scene.
    /// </summary>
    public void SpawnPlayer()
    {
        SessionData sessionData = SessionData.instance;
        ShipAsset asset = GameManager.Instance.playerSettings.shipsList.Where(x => x.stringID == sessionData.selectedShip.stringID).First();
        IPlayerInitialiser playerInitialiser = Instantiate(asset.shipPrefab, transform.position, Quaternion.identity).GetComponent<IPlayerInitialiser>();
        playerInitialiser.InitialisePlayer(this);
    }

    /// <summary>
    /// Spawns the team AI entities from the spawn managers.
    /// </summary>
    public void SpawnEnemyEntities()
    {

    }
}

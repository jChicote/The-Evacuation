using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using Evacuation.UserInterface.HUD;
using Evacuation.PlayerSystems;
using Evacuation.UserInterface.LocationMarker;
using Evacuation.Level.Collections;
using Evacuation.Actor.EnemySystems;

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
    [SerializeField] private GameObject locationManagerObject;

    // Serielised Inspector Fields
    [Space]
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameObject[] inhabitedSatellites;

    // Fields
    private GameManager gameManager;
    public IMarkerManager markerManager;
    private IActorTracker actorTracker;

    // Start is called before the first frame update
    private void Awake()
    {
        gameManager = GameManager.Instance;
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
        LoadInhabitedSatellites();
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
        markerManager = locationManagerObject.GetComponent<IMarkerManager>();
        markerManager.InitialiseMarkerManager();
    }

    public void RevealGameCompletionHUD()
    {
        completionHUD.gameObject.SetActive(true);
        completionHUD.InitialiseGameCompletionHUD(levelData);
    }

    /// <summary>
    /// Spawns the selected player into the scene.
    /// </summary>
    private void SpawnPlayer()
    {
        SessionData sessionData = SessionData.instance;
        ShipAsset asset = GameManager.Instance.playerSettings.shipsList.Where(x => x.stringID == sessionData.selectedShip.stringID).First();
        GameObject player = Instantiate(asset.shipPrefab, transform.position, Quaternion.identity);
        IPlayerInitialiser playerInitialiser = player.GetComponent<IPlayerInitialiser>();
        playerInitialiser.InitialisePlayer(this);

        actorTracker = Instantiate(GameManager.Instance.levelSettings.sceneActorTrackerPrefab, transform.position, Quaternion.identity).GetComponent<IActorTracker>();
        actorTracker.RegisterFriendlyEntity(player);
    }

    /// <summary>
    /// Spawns the team AI entities from the spawn managers.
    /// </summary>
    private void SpawnEnemyEntities()
    {
        // Test spawn
        GameObject spawnedDroid = Instantiate(gameManager.enemySettings.droidSentryPrefab, transform.position, Quaternion.identity);

        IAssignSceneActorTracker assignTracker = spawnedDroid.GetComponent<IAssignSceneActorTracker>();
        assignTracker.SetSceneActorTracker(actorTracker);
        actorTracker.RegisterEnemyEntity(spawnedDroid);

        IEnemyController enemyController = spawnedDroid.GetComponent<IEnemyController>();
        enemyController.InitialiseController();
    }
    
    /// <summary>
    /// Called to Load / Initialise island or ship related satellites of inhabitants.
    /// </summary>
    private void LoadInhabitedSatellites()
    {
        IAbstractInhabitants abstractInhabitants;
        ICarrierInitialiser carrierInitialiser = null;

        foreach (GameObject satellite in inhabitedSatellites)
        {
            abstractInhabitants = satellite.GetComponent<IAbstractInhabitants>();
            abstractInhabitants.InitialiseIsland(scoreSystem);
            carrierInitialiser = satellite.GetComponent<ICarrierInitialiser>();
        }

        InitialiseCarrier(carrierInitialiser);
    }

    private void InitialiseCarrier(ICarrierInitialiser carrierInitialiser)
    {
        if (carrierInitialiser == null) return;

        carrierInitialiser.InitialiseCarrier();
    }
}

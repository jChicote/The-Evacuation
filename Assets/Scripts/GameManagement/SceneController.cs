using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using Evacuation.UserInterface.HUD;
using Evacuation.Actor.PlayerSystems;
using Evacuation.UserInterface.LocationMarker;
using Evacuation.Level.Collections;
using Evacuation.Actor.EnemySystems;
using Evacuation.Session;
using Evacuation.Cinematics;

//  NOTICE FOR FUTURE REFERENCE
//
//  Functions associated with the scene controller myst be responsible for global and event based functions.
//  This must only be contained within the local scene and hold global reference to flyweight resources and 
//  core objects. This must not contain local entity based functions or functions that are not related to the
//  nature of this class. To maintain strict decoupling, initialisation of classes cannot be performed on the
//  scene controller and other local functions must be performed by the local object itself.
//
//  Initialisers are permitted

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
    [SerializeField] private CinematicManager cinematicManager;

    // Serielised Inspector Fields
    [Space]
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameObject[] inhabitedSatellites;
    [SerializeField] private GameObject[] enemySpawnManagers;
    [SerializeField] private GameObject playerSpawnManager;

    // Fields
    private GameManager gameManager;
    public IMarkerManager markerManager;
    private IActorTracker actorTracker;

    // Accessors
    public IActorTracker ActorTracker {  get { return actorTracker; } }
    public CinematicManager CinematicManager { get { return cinematicManager; } }

    // Start is called before the first frame update
    private void Awake()
    {
        gameManager = GameManager.Instance;
        gameManager.sceneController = this;

        OnGameCompletion = new UnityEvent();
        OnGameCompletion.AddListener(RevealGameCompletionHUD);

        LoadLevelData();
    }

    private void Start()
    {
        LoadHUD();
        LoadEntityTracker();
        LoadPlayerSpawner();
        LoadEnemySpawners();
        LoadInhabitedSatellites();
    }

    private void LoadLevelData()
    {
        // Currently we are loading directly from defaults 
        // In the future this should be transferred to a session data setup

        levelData = GameManager.Instance.levelSettings.defaultLevelData.Where(x => x.levelID == "#00091").First();
    }

    private void LoadLevelConfigurations() { }

    private void LoadEntityTracker()
    {
        actorTracker = Instantiate(GameManager.Instance.levelSettings.sceneActorTrackerPrefab, transform.position, Quaternion.identity).GetComponent<IActorTracker>();
        print("SceneController >> Entity Tracker Loaded");
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

    private void LoadPlayerSpawner()
    {
        playerSpawnManager = FindObjectOfType<PlayerSpawnManager>().gameObject;

        if (playerSpawnManager == null)
        {
            PlayerSettings playerSettings = gameManager.playerSettings;
            playerSpawnManager = Instantiate(playerSettings.playerSpawnManager, transform.position, Quaternion.identity);
        }

        ISpawnManager spawnManager = playerSpawnManager.GetComponent<ISpawnManager>();
        spawnManager.InitialisePlayerSpawner();
        print("Scene Controller >> Loaded Player Spawn Manager");
    }

    private void LoadEnemySpawners()
    {

    }

    /// <summary>
    /// Spawns the selected player into the scene.
    /// </summary>
    /*private void SpawnPlayer()
    {
        SessionData sessionData = SessionData.instance;
        ShipAsset asset = GameManager.Instance.playerSettings.shipsList.Where(x => x.stringID == sessionData.selectedShip.stringID).First();
        GameObject player = Instantiate(asset.shipPrefab, transform.position, Quaternion.identity);
        //IPlayerInitialiser playerInitialiser = player.GetComponent<IPlayerInitialiser>();
        //playerInitialiser.InitialisePlayer(this);

        actorTracker = Instantiate(GameManager.Instance.levelSettings.sceneActorTrackerPrefab, transform.position, Quaternion.identity).GetComponent<IActorTracker>();
        actorTracker.RegisterFriendlyEntity(player);
    }*/

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

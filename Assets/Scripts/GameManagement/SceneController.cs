using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class SceneController : MonoBehaviour
{
    public UnityEvent OnGameplayStart;
    public UnityEvent OnPlayerRespawn;
    public UnityEvent OnPlayerDeath;

    [Header("User Interfaces")]
    public PauseScreen pauseMenu;

    // Start is called before the first frame update
    private void Awake()
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.sceneController = this;

        //OnGameplayStart.AddListener(PrepareScene);
        PrepareScene();
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
        SpawnEntities();
    }

    /// <summary>
    /// Loads game HUD UI.
    /// </summary>
    private void LoadHUD()
    {
    }

    /// <summary>
    /// Spawns the selected player into the scene.
    /// </summary>
    public void SpawnPlayer()
    {
        SessionData sessionData = SessionData.instance;
        ShipAsset asset = GameManager.Instance.playerSettings.shipsList.Where(x => x.stringID == sessionData.selectedShip.stringID).First();
        Instantiate(asset.shipPrefab, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// Spawns the team AI entities from the spawn managers.
    /// </summary>
    public void SpawnEntities()
    {

    }
}

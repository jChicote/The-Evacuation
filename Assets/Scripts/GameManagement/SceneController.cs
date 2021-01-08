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

    // Start is called before the first frame update
    private void Awake()
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.sceneController = this;

        //OnGameplayStart.AddListener(PrepareScene);
    }

    /// <summary>
    /// Manages the sequential creation of important scene objects.
    /// </summary>
    /*private void PrepareScene()
    {
        LoadSceneCamera();
        LoadHUD();

        //Load Entities
        SpawnEntities();
    }*/

    /// <summary>
    /// Loads game HUD UI.
    /// </summary>
    private void LoadHUD()
    {
    }

    /// <summary>
    /// Spawns the team AI entities from the spawn managers.
    /// </summary>
    public void SpawnEntities()
    {
    }
}

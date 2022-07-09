using System.Collections.Generic;
using System.Linq;
using TheEvacuation.Common;
using TheEvacuation.Interfaces.GameInterfaces.Score;
using TheEvacuation.Spawner;
using UnityEngine;
using UnityEngine.Events;

namespace TheEvacuation.Infrastructure.GameSystems.SceneSystems
{

    public class SceneLevelManager : MonoBehaviour, ISceneDeathEventHandler, IScenePauseEventHandler
    {

        #region - - - - - - Fields - - - - - -

        public PlayerShipSpawner playerSpawner;
        public GameObject playerHealthBarGameObject;
        public SceneOperations sceneOperations;
        public ScorePresenter scorePresenter;
        public SceneScoreSystem sceneScoreSystem;

        [Space]
        public UnityEvent OnGameStart;
        public UnityEvent OnGamePause;
        public UnityEvent OnGameResume;
        public UnityEvent OnGameEnd;
        public UnityEvent OnPlayerDeath;

        [HideInInspector]
        public bool isPaused = false;

        #endregion Fields

        #region - - - - - - MonoBehaviour - - - - - -

        private void Awake()
        {
            playerSpawner = FindObjectOfType<PlayerShipSpawner>();
            sceneOperations = this.GetComponent<SceneOperations>();
            sceneScoreSystem = this.GetComponent<SceneScoreSystem>();
            scorePresenter = FindObjectOfType<ScorePresenter>();
        }

        private void Start()
        {
            GameManager.Instance.sceneLevelManager = this;

            sceneOperations.ConfigureSceneLevel(playerSpawner, sceneScoreSystem, scorePresenter);

            OnGameStart?.Invoke();
        }

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - - -

        public void PauseAllEntities()
        {
            GameObject[] allEntities = FindObjectsOfType<GameObject>();
            List<IPausable> pausableEntities = new List<IPausable>();

            foreach (GameObject entity in allEntities)
                pausableEntities.Union(entity.GetComponents<IPausable>().ToList());

            foreach (IPausable entity in pausableEntities)
            {
                if (isPaused)
                    entity.OnUnpauseEntity();
                else
                    entity.OnPauseEntity();
            }
        }

        public void ToggleGameDeath()
            => OnPlayerDeath?.Invoke();

        public void ToggleGamePause()
        {
            (!isPaused ? OnGamePause : OnGameResume)?.Invoke();
            isPaused = !isPaused;
        }

        #endregion Methods

    }

}

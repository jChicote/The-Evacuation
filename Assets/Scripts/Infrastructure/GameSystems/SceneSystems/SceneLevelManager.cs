using TheEvacuation.Common;
using TheEvacuation.InputSystem;
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
        public InputSystemManager inputSystemManager;

        [Space]
        public UnityEvent OnGameStart;
        public UnityEvent OnGamePause;
        public UnityEvent OnGameResume;
        public UnityEvent OnGameCompletion;
        public UnityEvent OnGameSaveExit;
        public UnityEvent OnGameNoSaveExit;
        public UnityEvent OnPlayerDeath;

        public bool IsPaused { get; set; } = false;

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
            MonoBehaviour[] allEntities = FindObjectsOfType<MonoBehaviour>();

            foreach (MonoBehaviour entity in allEntities)
                foreach (IPausable pausable in entity.GetComponents<IPausable>())
                    if (IsPaused)
                        pausable.OnPauseEntity();
                    else
                        pausable.OnUnpauseEntity();
        }

        public void ToggleGameDeath()
            => OnPlayerDeath?.Invoke();

        public void ToggleGameCompletion()
            => OnGameCompletion?.Invoke();

        public void ToggleGameSaveExit()
            => OnGameSaveExit?.Invoke();

        public void ToggleGameNoSaveExit()
            => OnGameNoSaveExit?.Invoke();

        public void ToggleGamePause()
        {
            if (IsPaused)
                OnGameResume?.Invoke();
            else
                OnGamePause?.Invoke();
        }

        #endregion Methods

    }

}

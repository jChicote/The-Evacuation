using TheEvacuation.Interfaces.GameInterfaces.Score;
using TheEvacuation.Spawner;
using UnityEngine;

namespace TheEvacuation.Infrastructure.GameSystems
{

    public class SceneLevelManager : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public PlayerShipSpawner playerSpawner;
        public GameObject playerHealthBarGameObject;
        public SceneOperations sceneOperations;
        public ScorePresenter scorePresenter;
        public SceneScoreSystem sceneScoreSystem;

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
        }

        #endregion MonoBehaviour

    }

}

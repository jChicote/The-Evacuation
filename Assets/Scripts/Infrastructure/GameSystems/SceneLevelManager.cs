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

        #endregion Fields

        #region - - - - - - MonoBehaviour - - - - - -

        private void Awake()
        {
            playerSpawner = FindObjectOfType<PlayerShipSpawner>();
            sceneOperations = this.GetComponent<SceneOperations>();
        }

        private void Start()
        {
            GameManager.Instance.sceneLevelManager = this;

            sceneOperations.ConfigureSceneLevel(playerSpawner);
        }

        #endregion MonoBehaviour

    }

}

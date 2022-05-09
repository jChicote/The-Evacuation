using TheEvacuation.Spawner;
using UnityEngine;

namespace TheEvacuation.Infrastructure.GameSystems
{

    /// <summary>
    /// Handles general scene setup, tied to Unity's order of execution
    /// </summary>
    public class SceneOperations : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        private PlayerShipSpawner playerSpawner;

        #endregion Fields

        #region - - - - - - MonoBehaviour - - - - - -

        private void Awake()
        {
            playerSpawner = FindObjectOfType<PlayerShipSpawner>();
        }

        private void Start()
        {
            // Initialise the player spawner
            if (GameManager.Instance.SessionData.SelectedSpaceShip != null)
            {
                playerSpawner.IntialisePlayerSpawner(GameManager.Instance.SessionData.SelectedSpaceShip);
                playerSpawner.CreateEntityInstance();
            }

        }

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - - -

        public void OnSceneClosing()
        {

        }

        #endregion Methods

    }

}
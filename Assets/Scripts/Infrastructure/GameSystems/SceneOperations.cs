using TheEvacuation.Spawner;
using UnityEngine;

namespace TheEvacuation.Infrastructure.GameSystems
{

    /// <summary>
    /// Handles general scene setup, tied to Unity's order of execution
    /// </summary>
    public class SceneOperations : MonoBehaviour
    {

        #region - - - - - - Methods - - - - - -

        public void ConfigureSceneLevel(PlayerShipSpawner playerSpawner)
        {
            // Initialise the player spawner
            if (GameManager.Instance.SessionData.SelectedSpaceShip != null)
            {
                playerSpawner.IntialisePlayerSpawner(GameManager.Instance.SessionData.SelectedSpaceShip);
                playerSpawner.CreateEntityInstance();
            }
        }

        public void OnSceneClosing()
        {

        }

        #endregion Methods

    }

}
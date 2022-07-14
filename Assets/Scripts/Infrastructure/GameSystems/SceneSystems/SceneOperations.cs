using TheEvacuation.Interfaces.GameInterfaces.Score;
using TheEvacuation.Spawner;
using UnityEngine;

namespace TheEvacuation.Infrastructure.GameSystems.SceneSystems
{

    /// <summary>
    /// Handles general scene setup, tied to Unity's order of execution
    /// </summary>
    public class SceneOperations : MonoBehaviour
    {

        #region - - - - - - Methods - - - - - -

        public void ConfigureSceneLevel(
            PlayerShipSpawner playerSpawner,
            SceneScoreSystem sceneScoreSystem,
            ScorePresenter scorePresenter)
        {
            sceneScoreSystem.InitialiseSceneScoreSystem(scorePresenter);

            // Initialise the player spawner
            if (GameManager.Instance.SessionData.SelectedSpaceShip != null)
            {
                playerSpawner.IntialisePlayerSpawner(GameManager.Instance.SessionData.SelectedSpaceShip);
                playerSpawner.CreateEntityInstance();
            }
        }

        public void ExitScene(string sceneName)
            => GameManager.Instance.sceneLoader.LoadNextScene(sceneName);

        #endregion Methods

    }

}
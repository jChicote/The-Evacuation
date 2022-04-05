using TheEvacuation.Infrastructure.GameSystems;
using TheEvacuation.Model.Entities;
using TheEvacuation.ScriptableObjects.FlyweightSettings;
using TheEvacuation.Spawner;
using UnityEngine;

namespace TheEvacuation.Tests.GameMode
{

    public class Test_PlayerSpawnerInvoker : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public PlayerFlyweightSettings playerFlyweightSettings;
        public PlayerSpawner playerSpawner;
        public GameManager gameManager;
        public int spaceShipIdentifier = 0;

        #endregion Fields

        #region - - - - - - MonoBehaviour - - - - - -

        private void Awake()
        {
            gameManager = GameManager.Instance;
        }

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - - -

        public void CreateEntityInstance_SpawnSpaceShipEntity_EntitySpawned()
        {
            // Arrange
            SpaceShip spaceShip = new SpaceShip()
            {
                identifier = spaceShipIdentifier
            };

            playerSpawner.IntialisePlayerSpawner(spaceShip);

            // Act
            playerSpawner.CreateEntityInstance();

            // Assert
        }

        #endregion Methods

    }

}
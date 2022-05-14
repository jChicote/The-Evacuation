using System.Linq;
using TheEvacuation.Character.ConfigurationDispatcher;
using TheEvacuation.Character.ConfigurationDispatcher.Player;
using TheEvacuation.Infrastructure.GameSystems;
using TheEvacuation.Interfaces.GameInterfaces.VitalityBars;
using TheEvacuation.Model.Entities;
using TheEvacuation.ScriptableObjects.FlyweightSettings;
using UnityEngine;

namespace TheEvacuation.Spawner
{

    public class PlayerShipSpawner : Spawner, IPlayerSpawner
    {

        #region - - - - - - Fields - - - - - -

        public PlayerFlyweightSettings playerFlyweightSettings;
        public SpaceShip spaceShip;
        public GameObject spaceShipShell;

        public bool hasSpawned = false;
        public bool canSpawnOnStart = false;

        #endregion Fields

        #region - - - - - - Initialiser - - - - - -

        public void IntialisePlayerSpawner(SpaceShip spaceShip)
        {
            this.spaceShip = spaceShip;
            GetSpaceShipShellPrefabByIdentifier(spaceShip.identifier);
        }

        #endregion Initialiser

        #region - - - - - - MonoBehaviour - - - - - -

        private void Start()
        {
            if (canSpawnOnStart && spaceShipShell != null)
                CreateEntityInstance();
        }

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - - -

        public override GameObject CreateEntityInstance()
        {
            if (IsPaused)
                return null;

            GameObject player = Instantiate(spaceShipShell, Vector3.zero, Quaternion.identity);
            SceneLevelManager sceneLevelManager = GameManager.Instance.sceneLevelManager;

            PlayerInputConfigurationPort inputPort = new PlayerInputConfigurationPort()
            {
                SpaceShip = this.spaceShip.Clone(),
                HealthBar = sceneLevelManager.playerHealthBarGameObject.GetComponent<IPlayerHealthBar>()
            };

            player.GetComponent<IConfigurationDispatcher<PlayerInputConfigurationPort>>().ConfigureGameObjectSystems(inputPort);

            return player;
        }

        public void GetSpaceShipShellPrefabByIdentifier(int id)
        {
            spaceShipShell = playerFlyweightSettings.shipPrefabs
                                .Where(sp => sp.identifier == id)
                                .SingleOrDefault()
                                .shipPrefab;

            Debug.Log(spaceShipShell);

            if (spaceShipShell == null)
                Debug.LogError("Space Ship Shell not found with identifier: " + id);
        }

        #endregion Methods

    }

}

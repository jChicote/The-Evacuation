using System.Linq;
using TheEvacuation.Model.Entities;
using TheEvacuation.PlayerSystems.Movement;
using TheEvacuation.ScriptableObjects.FlyweightSettings;
using UnityEngine;

namespace TheEvacuation.Spawner
{

    public class PlayerSpawner : Spawner
    {

        #region - - - - - - Fields - - - - - -

        public PlayerFlyweightSettings playerFlyweightSettings;
        public SpaceShip spaceShip;
        public GameObject spaceShipShell;

        public bool hasSpawned = false;

        #endregion Fields

        #region - - - - - - Initialiser - - - - - -

        public void IntialisePlayerSpawner(SpaceShip spaceShip)
        {
            this.spaceShip = spaceShip;
            GetSpaceShipShellPrefabByIdentifier(spaceShip.identifier);
        }

        #endregion Initialiser

        #region - - - - - - Methods - - - - - -

        public override GameObject CreateEntityInstance()
        {
            if (IsPaused)
                return null;

            GameObject player = Instantiate(spaceShipShell, Vector3.zero, Quaternion.identity);
            player.GetComponent<IShipMovementSystem>().InitialiseShipMovementSystem(spaceShip.shipAttributes);

            return player;
        }

        public void GetSpaceShipShellPrefabByIdentifier(int id)
        {
            spaceShipShell = playerFlyweightSettings.shipPrefabs
                                .Where(sp => sp.identifier == id)
                                .SingleOrDefault()
                                .shipShell;

            if (spaceShipShell == null)
                Debug.LogError("Space Ship Shell not found with identifier: " + id);
        }

        #endregion Methods

    }

}

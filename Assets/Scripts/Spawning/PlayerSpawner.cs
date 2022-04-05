using System.Linq;
using TheEvacuation.Model.Entities;
using TheEvacuation.ScriptableObjects.FlyweightSettings;
using UnityEngine;

namespace TheEvacuation.Spawner
{

    public interface IPlayerSpawner : ISpawner
    {

        #region - - - - - - Methods - - - - - -

        void IntialisePlayerSpawner(SpaceShip spaceShip);

        #endregion Methods

    }

    public class PlayerSpawner : Spawner
    {

        #region - - - - - - Fields - - - - - -

        public PlayerFlyweightSettings playerFlyweightSettings;

        public SpaceShip spaceShip;
        public GameObject spaceShipShell;
        public bool hasSpawned = false;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void IntialisePlayerSpawner(SpaceShip spaceShip) // Equivalent of a constructor
        {
            this.spaceShip = spaceShip;
            GetSpaceShipShellPrefabByIdentifier(spaceShip.identifier);
        }

        public override GameObject CreateEntityInstance()
        {
            /*
             * This implementation is basic at the moment but will need implementation to inject values into the class
             */
            GameObject player = Instantiate(spaceShipShell, Vector3.zero, Quaternion.identity);
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

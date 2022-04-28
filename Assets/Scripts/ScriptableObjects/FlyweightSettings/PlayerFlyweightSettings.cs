using TheEvacuation.Model.Entities;
using UnityEngine;

namespace TheEvacuation.ScriptableObjects.FlyweightSettings
{

    [CreateAssetMenu(menuName = "Settings/Player Flyweight Settings")]
    public class PlayerFlyweightSettings : ScriptableObject
    {

        #region - - - - - - Prefabs - - - - - -

        public ShipAsset[] shipPrefabs;

        #endregion

        #region - - - - - - Methods - - - - - -

        public ShipAsset GetShipAssetFromIdentifier(int identifier)
        {
            ShipAsset prefab = shipPrefabs[0];

            for (int i = 0; i < shipPrefabs.Length; i++)
            {
                if (shipPrefabs[i].identifier == identifier)
                    prefab = shipPrefabs[i];
            }

            return prefab;
        }

        #endregion Methods

    }

}

using UnityEngine;

namespace TheEvacuation.ScriptableObjects.FlyweightSettings
{

    [CreateAssetMenu(menuName = "Settings/Player Flyweight Settings")]
    public class PlayerFlyweightSettings : ScriptableObject
    {

        #region - - - - - - Prefabs - - - - - -

        public GameObject[] shipPrefabs;

        #endregion

    }

}

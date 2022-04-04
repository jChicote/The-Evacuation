using TheEvacuation.ScriptableObjects.FlyweightSettings;
using UnityEngine;

namespace TheEvacuation.Spawner
{

    public class PlayerSpawner : Spawner
    {

        #region - - - - - - Fields - - - - - -

        public PlayerFlyweightSettings playerFlyweightSettings;

        #endregion

        #region - - - - - - Methods - - - - - -

        public override GameObject CreateEntityInstance()
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }

}

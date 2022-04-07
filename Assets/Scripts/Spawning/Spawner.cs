using TheEvacuation.Common;
using UnityEngine;

namespace TheEvacuation.Spawner
{

    public abstract class Spawner : BaseInteractiveObject, ISpawner
    {

        #region - - - - - - Methods - - - - - -

        public abstract GameObject CreateEntityInstance();

        #endregion

    }

}

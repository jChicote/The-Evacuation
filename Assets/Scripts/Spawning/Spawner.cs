using TheEvacuation.Common;
using UnityEngine;

namespace TheEvacuation.Spawner
{

    public interface ISpawner
    {

        #region - - - - - - Methods - - - - - -

        GameObject CreateEntityInstance();

        #endregion

    }

    public abstract class Spawner : BaseInteractiveObject, ISpawner
    {

        #region - - - - - - Methods - - - - - -

        public abstract GameObject CreateEntityInstance();

        #endregion

    }

}

using UnityEngine;

namespace TheEvacuation.Spawner
{
    public interface ISpawner
    {

        #region - - - - - - Methods - - - - - -

        GameObject CreateEntityInstance();

        #endregion

    }

}

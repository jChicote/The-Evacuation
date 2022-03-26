using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace TheEvacuation.Infrastructure.Persistence
{

    public abstract class DataContext : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public GameData data = new GameData();

        #endregion

        #region - - - - - - Methods - - - - - -

        public abstract Task Load();
        public abstract Task Save();

        public List<TBase> Set<TBase>()
        {
            // Implementation missing

            return null;
        }

        #endregion Methods

    }

}

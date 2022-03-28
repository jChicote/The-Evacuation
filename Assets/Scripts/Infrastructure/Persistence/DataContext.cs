using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheEvacuation.Model.Entities;
using UnityEngine;

namespace TheEvacuation.Infrastructure.Persistence
{

    [Serializable]
    public abstract class DataContext : MonoBehaviour, IDataContext
    {

        #region - - - - - - Fields - - - - - -

        public GameData data = new GameData();

        #endregion

        #region - - - - - - Methods - - - - - -

        public abstract Task Load();
        public abstract Task Save();

        public List<TBase> Set<TBase>()
        {
            if (typeof(TBase) == typeof(Player))
                return data.m_Players as List<TBase>;

            return null;
        }

        #endregion Methods

    }

}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheEvacuation.Model.Entities;
using UnityEngine;

namespace TheEvacuation.Infrastructure.Persistence
{

    /// <summary>
    /// DataContext is the source of all entities and is made to retrieve and cache entities.
    /// </summary>
    [Serializable]
    public abstract class DataContext : MonoBehaviour, IDataContext
    {

        #region - - - - - - Fields - - - - - -

        public GameData data = new GameData();

        #endregion

        #region - - - - - - Methods - - - - - -

        public void CreateNewGameData()
        {
            data = new GameData()
            {
                m_Players = new List<Player>()
            };
        }

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

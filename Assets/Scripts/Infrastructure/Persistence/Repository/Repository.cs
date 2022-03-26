using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheEvacuation.Model.Entities;
using UnityEngine;

namespace TheEvacuation.Infrastructure.Persistence.Repository
{

    public abstract class Repository<TBase> : MonoBehaviour, IRepository<TBase> where TBase : BaseEntity
    {

        #region - - - - - - Fields - - - - - -

        public DataContext context;

        #endregion

        #region - - - - - - Properties - - - - - -

        public List<TBase> Entities => context.Set<TBase>();

        #endregion

        #region - - - - - - Methods - - - - - -

        public TBase GetById(Guid id)
            => Entities.FirstOrDefault(e => e.ID == id);

        public void Add(TBase entity)
            => Entities.Add(entity);

        public void Delete(TBase entity)
            => Entities.Remove(entity);

        public void Modify(TBase entity)
            => Entities[Entities.FindIndex(e => e.ID == entity.ID)] = entity;

        public async Task Save()
            => await context.Save();

        #endregion Methods

    }

}

using System;
using TheEvacuation.Model.Entities;

namespace TheEvacuation.Infrastructure.Persistence.Repository
{

    public interface IRepository<TBase> where TBase : BaseEntity
    {

        #region - - - - - - Methods - - - - - -

        TBase GetById(Guid id);
        void Add(TBase entity);
        void Delete(TBase entity);
        void Modify(TBase entity);

        #endregion

    }

}
using System;

namespace TheEvacuation.Model.Entities
{

    public interface ICloneable<TEntity> where TEntity : class
    {

        #region - - - - - - Methods - - - - - -

        TEntity Clone();

        TEntity ShallowClone();

        #endregion Methods

    }

    [Serializable]
    public class BaseEntity
    {

        #region - - - - - - Properties - - - - - -

        public Guid ID { get; set; }

        #endregion Properties

    }

}
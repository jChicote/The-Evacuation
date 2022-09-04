using System;

namespace TheEvacuation.Model.Entities
{

    public class Level : BaseEntity, ICloneable<Level>
    {

        #region - - - - - - Fields - - - - - -

        public Guid ScoreBoardID;
        public int SceneID;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public Level() =>
            ID = new Guid();

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public Level Clone()
        {
            Level clone = ShallowClone();
            return clone;
        }

        public Level ShallowClone()
            => this.MemberwiseClone() as Level;

        #endregion Methods

    }

}
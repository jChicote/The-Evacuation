using System;

namespace TheEvacuation.Model.Entities
{

    [Serializable]
    public class SpaceShip : ICloneable<SpaceShip>
    {

        #region - - - - - - Fields - - - - - -

        public int identifier;
        public ShipAttributes shipAttributes;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public SpaceShip Clone()
        {
            SpaceShip clone = ShallowClone();
            clone.shipAttributes = this.shipAttributes;

            return clone;
        }

        public SpaceShip ShallowClone()
            => this.MemberwiseClone() as SpaceShip;

        #endregion Methods

    }

}
using System;

namespace TheEvacuation.Model.Entities
{

    [Serializable]
    public class SpaceShip : ICloneable<SpaceShip>
    {

        #region - - - - - - Fields - - - - - -

        public int identifier;
        public string name;
        public ShipAttributes shipAttributes;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public SpaceShip Clone()
        {
            SpaceShip clone = ShallowClone();
            clone.name = String.Copy(this.name);
            clone.shipAttributes = this.shipAttributes.Clone();

            return clone;
        }

        public SpaceShip ShallowClone()
            => this.MemberwiseClone() as SpaceShip;

        #endregion Methods

    }

}
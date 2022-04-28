using System;

namespace TheEvacuation.Model.Entities
{

    [Serializable]
    public class ShipAttributes : ICloneable<ShipAttributes>
    {

        #region - - - - - - Fields - - - - - -

        public float maxSpeed;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public ShipAttributes Clone()
        {
            ShipAttributes clone = ShallowClone();
            clone.maxSpeed = this.maxSpeed;
            return clone;
        }

        public ShipAttributes ShallowClone()
            => this.MemberwiseClone() as ShipAttributes;

        #endregion Methods

    }

}

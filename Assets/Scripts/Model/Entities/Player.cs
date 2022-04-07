using System;

namespace TheEvacuation.Model.Entities
{

    [Serializable]
    public class Player : BaseEntity
    {

        #region - - - - - - Fields - - - - - -

        public string name;
        public SpaceShip[] spaceShipHanger;

        #endregion Fields

    }

}

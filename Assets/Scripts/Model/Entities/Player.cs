using System;
using System.Collections.Generic;

namespace TheEvacuation.Model.Entities
{

    [Serializable]
    public class Player : BaseEntity
    {

        #region - - - - - - Fields - - - - - -

        public string name;
        public List<SpaceShip> spaceShipHanger;
        public int avatarIdentifier;
        public PlayerStatistics statistics;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public Player ShallowCopy()
            => this.MemberwiseClone() as Player;

        public Player Clone()
        {
            Player clone = ShallowCopy();
            clone.name = String.Copy(this.name);

            return clone;
        }

        #endregion Methods

    }

}

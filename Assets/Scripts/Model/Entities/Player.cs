using System;
using System.Collections.Generic;
using System.Linq;

namespace TheEvacuation.Model.Entities
{

    [Serializable]
    public class Player : BaseEntity, ICloneable<Player>
    {

        #region - - - - - - Fields - - - - - -

        public string name;
        public List<SpaceShip> spaceShipHanger;
        public int avatarIdentifier;
        public PlayerStatistics statistics;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public Player ShallowClone()
            => this.MemberwiseClone() as Player;

        public Player Clone()
        {
            Player clone = ShallowClone();
            clone.name = String.Copy(this.name);
            clone.spaceShipHanger = spaceShipHanger.Select(ssh => ssh.Clone()).ToList();
            clone.statistics = statistics.Clone();

            return clone;
        }

        #endregion Methods

    }

}

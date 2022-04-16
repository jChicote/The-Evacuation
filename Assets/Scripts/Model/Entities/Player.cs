using System;
using UnityEngine;

namespace TheEvacuation.Model.Entities
{

    [Serializable]
    public class Player : BaseEntity
    {

        #region - - - - - - Fields - - - - - -

        public string name;
        public SpaceShip[] spaceShipHanger;
        public Sprite avatarImage;
        public PlayerStatistics statistics;

        #endregion Fields

    }

}

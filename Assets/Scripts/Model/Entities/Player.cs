using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheEvacuation.Model.Entities
{

    [Serializable]
    public class Player : BaseEntity
    {

        #region - - - - - - Fields - - - - - -

        public string name;
        public List<SpaceShip> spaceShipHanger;
        public Sprite avatarImage;
        public PlayerStatistics statistics;

        #endregion Fields

    }

}

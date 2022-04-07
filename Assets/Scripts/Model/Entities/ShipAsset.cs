using System;
using UnityEngine;

namespace TheEvacuation.Model.Entities
{

    [Serializable]
    public class ShipAsset
    {

        #region - - - - - - Fields - - - - - -

        public string name;
        public int identifier;
        public SpaceShip shipDefaults;
        public GameObject shipShell;

        #endregion Fields

    }

}

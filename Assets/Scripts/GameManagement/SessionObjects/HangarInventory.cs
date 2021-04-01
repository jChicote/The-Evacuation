using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Actor;

namespace Evacuation.Session
{
    [System.Serializable]
    public class HangarInventory
    {
        public List<WeaponInfo> hangarWeapons = new List<WeaponInfo>();
        public List<ShipInfo> hangarShips = new List<ShipInfo>();
    }
}


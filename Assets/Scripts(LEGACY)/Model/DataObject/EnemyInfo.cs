using System.Collections;
using System.Collections.Generic;
using System;

namespace Evacuation.Actor.EnemySystems
{
    [Serializable]
    public class EnemyInfo : ShipInfo
    {
        public List<string> weaponList;

        public void SetData(string stringID, string name, float maxHealth, float maxShield, float maxSpeed, float maxHandling, int weaponSize)
        {
            this.instanceID = stringID;
            this.name = name;

            this.maxHealth = maxHealth;
            this.maxHealth = maxShield;
            this.maxSpeed = maxSpeed;
            this.maxHandling = maxHandling;

            this.weaponList = new List<string>(new string[weaponSize]);
            PopulateListToDefault(weaponList);
        }
    }

    [Serializable]
    public class EnemyShipStats : BaseStats
    {
        public int weaponSize;
    }
}

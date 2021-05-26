using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Model;
using Evacuation.Model.Data;

namespace Evacuation.Actor
{
    public interface IAssignWeapon
    {
        /// <summary>
        /// Called to assign weapons to the specified loadout position.
        /// </summary>
        void AssignWeapons(WeaponConfiguration weaponConfig, string weaponID);

        /// <summary>
        /// Called to remove the weapons from the specified loadout and index.
        /// </summary>
        void RemoveWeapon(WeaponConfiguration weaponConfig, string stringID);
    }

    /// <summary>
    /// A class containng the fields necessary for the ship and UTILISED for data storage.
    /// </summary>
    [System.Serializable]
    public class ShipInfo : ObjectInfo, IAssignWeapon
    {
        // This class is a more lightweight configuration made for storage on device locally and does not draw from settings.
        public bool isUnlocked = false;

        //public ShipData data;

        // Core Stats
        public int rescueCapacity;
        public float maxHealth;
        public float maxSheild;
        public float maxSpeed;
        public float maxHandling;

        // Weapoon Loadouts
        public List<string> fixedWeapons;
        public List<string> turrentWeapons;

        public void SetData(string stringID, string name, int price, int rescueCapacity, float maxHealth, float maxShield, float maxSpeed, float maxHandling, int forwardWeaponSize, int turrentWeaponSize)
        {
            this.stringID = stringID;
            this.name = name;
            this.price = price;

            this.rescueCapacity = rescueCapacity;
            this.maxHealth = maxHealth;
            this.maxHealth = maxShield;
            this.maxSpeed = maxSpeed;
            this.maxHandling = maxHandling;

            this.fixedWeapons = new List<string>(new string[forwardWeaponSize]);
            PopulateListToDefault(fixedWeapons);
            this.turrentWeapons = new List<string>(new string[turrentWeaponSize]);
            PopulateListToDefault(turrentWeapons);
        }

        public ShipData GetShipData()
        {
            ShipData dataObject = new ShipData(rescueCapacity, maxHealth, maxSheild, maxSpeed, maxHandling, fixedWeapons, turrentWeapons);
            return dataObject;
        }

        /// <summary>
        /// Called to populate a weapon list to default empty values.
        /// </summary>
        /// <param name="list"></param>
        protected void PopulateListToDefault(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = "";
            }
        }

        /// <summary>
        /// Called to assign weapons to the specified weapon configuration.
        /// </summary>
        public void AssignWeapons(WeaponConfiguration weaponConfig, string weaponID)
        {
            if (weaponConfig == WeaponConfiguration.Forward)
            {
                AllocateWeaponToList(weaponID, this.fixedWeapons);
            }
            else if (weaponConfig == WeaponConfiguration.Turrent)
            {
                AllocateWeaponToList(weaponID, this.turrentWeapons);
            }
        }

        /// <summary>
        /// Allocates weapons to their their position if existent.
        /// </summary>
        protected void AllocateWeaponToList(string weaponID, List<string> weaponList)
        {
            List<WeaponInfo> hangarWeapons = SessionData.instance.weaponServicer.GetHangarWeapons();
            int indexPosition = GetFirstEmptySlot(weaponList);
            string extractedWeaponID = "";

            for (int i = 0; i < hangarWeapons.Count; i++)
            {
                if (extractedWeaponID == "")
                {
                    if (hangarWeapons[i].stringID.Equals(weaponID) && !hangarWeapons[i].isAttached)
                    {
                        extractedWeaponID = hangarWeapons[i].stringID;
                        hangarWeapons[i].isAttached = true;
                    }
                }
            }

            // Check if array search turned up with a result.
            if (extractedWeaponID != "")
            {
                //extractedWeaponID.isAttached = true;
                weaponList[indexPosition] = extractedWeaponID;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void RemoveWeapon(WeaponConfiguration weaponConfig, string stringID)
        {
            List<WeaponInfo> hangarWeapons = SessionData.instance.weaponServicer.GetHangarWeapons();

            if (weaponConfig == WeaponConfiguration.Forward)
            {
                RemoveWeaponFromList(fixedWeapons, stringID);
            }
            else
            {
                RemoveWeaponFromList(turrentWeapons, stringID);
            }
        }

        private void RemoveWeaponFromList(List<string> weaponList, string stringID)
        {
            List<WeaponInfo> hangarWeapons = SessionData.instance.weaponServicer.GetHangarWeapons();

            int indexPosition = GetEquipmentPosition(weaponList, stringID);
            weaponList[indexPosition] = "";

            // Changes info in weapon hangar to be unattached
            hangarWeapons.Where(x => x.stringID == stringID).First().isAttached = false;
        }

        /// <summary>
        /// Determines the index of the first available slot.
        /// </summary>
        protected int GetFirstEmptySlot(List<string> weaponList)
        {
            int availableIndex = 100000;
            bool isAvailable = false;

            for (int i = 0; i < weaponList.Count; i++)
            {
                if (weaponList[i] == "" && !isAvailable)
                {
                    availableIndex = i;
                    isAvailable = true;
                }
            }

            return availableIndex;
        }

        /// <summary>
        /// Gets the equipment position in the list.
        /// </summary>
        protected int GetEquipmentPosition(List<string> list, string equipmentID)
        {
            int locatedIndex = 100000;
            bool isLocated = false;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == equipmentID && !isLocated)
                {
                    locatedIndex = i;
                    isLocated = true;
                }
            }

            return locatedIndex;
        }

        /// <summary>
        /// Gets the weapon ships from the ship based on the specified configuration.
        /// </summary>
        public List<string> GetWeaponsList(WeaponConfiguration configuration)
        {
            if (configuration == WeaponConfiguration.Forward)
            {
                return this.fixedWeapons;
            }
            else
            {
                return this.turrentWeapons;
            }
        }

        /// <summary>
        /// Checks if all weapon arrays are full.
        /// </summary>
        public bool CheckIsFull(WeaponConfiguration configuration)
        {
            if (configuration == WeaponConfiguration.Forward)
            {
                return this.fixedWeapons.Where(x => x == "").Count() == 0;
            }
            else
            {
                return this.turrentWeapons.Where(x => x == "").Count() == 0;
            }
        }

        public void UnlockShip(bool state)
        {
            isUnlocked = state;
        }
    }

    public enum WeaponConfiguration
    {
        Forward,
        Turrent
    }
}
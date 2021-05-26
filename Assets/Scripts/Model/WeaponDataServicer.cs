using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Manages operations of the weapon hangar. Such hangar weapons are the entire collection
// of weapons accessible and purchased in the game. This includes both inventory weapons 
// and weapons attached to vessels.

namespace Evacuation.Model
{
    public interface IWeaponServicer
    {
        List<WeaponInfo> GetHangarWeapons();
        WeaponInfo GetWeaponItem(string weaponID);
        int GetAvailableWeaponInstanceCount(string universalID);
        void RemoveWeaponInstance(string universalID);
        void AddWeaponInstance(WeaponAsset asset);
    }

    public class WeaponDataServicer : MonoBehaviour, IWeaponServicer
    {
        // Responisble for handling logic associated with weapon organisation in data and 
        // retruieval of data items from arrays.

        private List<WeaponInfo> hangarWeapons;

        public WeaponDataServicer()
        {
            hangarWeapons = new List<WeaponInfo>();
        }

        /// <summary>
        /// Sets the hangar weapons. Preferably after initialisation or loading
        /// </summary>
        public void SetHangarShips(List<WeaponInfo> loadedWeapons)
        {
            this.hangarWeapons = loadedWeapons;
        }

        /// <summary>
        /// Accessor for hangar weapons
        /// </summary>
        public List<WeaponInfo> GetHangarWeapons()
        {
            return hangarWeapons;
        }

        /// <summary>
        /// Gets the weapon info of the specified weapon
        /// </summary>
        public WeaponInfo GetWeaponItem(string weaponID)
        {
            // To limit the clunckiness of passing weapon info around, each vessel will instead store only the string reference.
            return hangarWeapons.Where(x => x.stringID == weaponID).First();
        }

        /// <summary>
        /// Gets the available weapon count in the hangar
        /// </summary>
        public int GetAvailableWeaponInstanceCount(string universalID)
        {
            return hangarWeapons.Where(x => x.universalID == universalID && x.isAttached == false).Count();
        }

        /// <summary>
        /// Removes weapon instance from list
        /// </summary>
        public void RemoveWeaponInstance(string universalID)
        {
            hangarWeapons.Remove(hangarWeapons.Where(x => x.universalID == universalID && x.isAttached == false).First());
        }

        /// <summary>
        /// Adds weapon instance into hangar
        /// </summary>
        public void AddWeaponInstance(WeaponAsset asset)
        {
            hangarWeapons.Add(asset.ConvertToWeaponInfo());
        }
    }
}

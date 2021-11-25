using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Model;

namespace Evacuation.Actor.PlayerSystems
{
    /// <summary>
    /// Placed within loadout position that both sets and gets the weapon in the loadout
    /// </summary>
    public class LoadoutHolder : MonoBehaviour
    {
        // Inspector accessible fields
        [SerializeField] private LoadoutConfiguration loadoutType;

        // Fields
        private GameObject weapon;

        /// <summary>
        /// Is given a loadout configuration which then loads the weapon.
        /// </summary>
        public IWeapon SetWeapon(string weaponID)
        {
            // Grab asset from the scriptable object
            WeaponInfo info = SessionData.instance.weaponServicer.GetWeaponItem(weaponID);
            WeaponAsset weaponAsset = GameManager.Instance.weaponSettings.RetrieveFromSettings(info.weaponType, info.globalID);

            if (weaponAsset == null) return null;

            return InitialiseWeapon(weaponAsset, info);
        }

        private IWeapon InitialiseWeapon(WeaponAsset weaponAsset, WeaponInfo info)
        {
            //Spawn and Initialise Weapon
            weapon = Instantiate(weaponAsset.weaponPrefab, transform);
            IWeapon weaponInterface = weapon.GetComponent<IWeapon>();
            weaponInterface.InitialiseWeapon(info, transform.root.gameObject.GetComponent<IEntitySpeed>());
            weaponInterface.ConfigureWeaponPositioning(loadoutType);

            return weaponInterface;
        }
    }
}


//   MOVE TO A SEPERATE FILE        

public class Loadout
{
    public int positionNum;
    public WeaponAsset weaponInformation;
}

public enum LoadoutConfiguration
{
    Forward,
    Pivot
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSystems
{
    /// <summary>
    /// Placed within loadout position that both sets and gets the weapon in the loadout
    /// </summary>
    public class LoadoutHolder : MonoBehaviour
    {
        public int loadoutNum;
        public LoadoutPosition loadoutType;
        public Loadout weaponLoadout;
        public GameObject weapon;

        /// <summary>
        /// Is given a loadout configuration which then loads the weapon.
        /// </summary>
        public IWeapon SetWeapon(string weaponID)
        {
            // Grab asset from the scriptable object
            WeaponInfo info = SessionData.instance.weaponServicer.GetWeaponItem(weaponID);
            WeaponAsset weaponAsset = GameManager.Instance.weaponSettings.RetrieveFromSettings(info.weaponType, info.universalID);

            if (weaponAsset == null) return null;

            //Spawn and Initialise Weapon
            weapon = Instantiate(weaponAsset.weaponPrefab, transform);
            IWeapon weaponInterface = weapon.GetComponent<IWeapon>();
            weaponInterface.InitialiseWeapon(info, transform.root.gameObject.GetComponent<IMovementAccessors>());
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

public enum LoadoutPosition
{
    Forward,
    Pivot
}


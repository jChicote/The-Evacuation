using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class WeaponSetupHandler
    {
        /// <summary>
        /// Setup the forward weapons with the given forward weapon loadout.
        /// </summary>
        public void SetupForwardWeapons(List<IWeapon> weapons, LoadoutHolder[] forwardWeapons)
        {
            // TEMPORARY: ALL WEAPONS WILL BE SET TO DEFAULT BEFORE STAT HANDLER IMPLEMENTATION
            WeaponSettings settings = GameManager.Instance.weaponSettings;
            Loadout loadout = new Loadout();

            foreach (LoadoutHolder holder in forwardWeapons)
            {
                loadout.weaponInformation = settings.turrentWeapons[0];
                weapons.Add(holder.SetWeapon(loadout));
            }
        }

        /// <summary>
        /// Setup the turrent weapons with the given turrent weapon loadout.
        /// </summary>
        public void SetupTurrentWeapons(List<IWeapon> weapons, LoadoutHolder[] turrentWeapons)
        {
            // TEMPORARY: ALL WEAPONS WILL BE SET TO DEFAULT BEFORE STAT HANDLER IMPLEMENTATION
            WeaponSettings settings = GameManager.Instance.weaponSettings;
            Loadout loadout = new Loadout();

            foreach (LoadoutHolder holder in turrentWeapons)
            {
                loadout.weaponInformation = settings.turrentWeapons[0];
                weapons.Add(holder.SetWeapon(loadout));
            }
        }
    }
}

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
        public void SetupForwardWeapons(List<IWeapon> weapons, Loadout[] loadoutCompliment, LoadoutHolder[] forwardWeapons)
        {
            for (int i = 0; i < forwardWeapons.Length; i++)
            {
                //Check if in bounds
                if (CheckInBounds(i, loadoutCompliment.Length)) {
                    weapons.Add(forwardWeapons[i].SetWeapon(loadoutCompliment[i]));
                }
            }
        }

        /// <summary>
        /// Setup the turrent weapons with the given turrent weapon loadout.
        /// </summary>
        public void SetupTurrentWeapons(List<IWeapon> weapons, Loadout[] loadoutCompliment, LoadoutHolder[] turrentWeapons)
        {
            for (int i = 0; i < turrentWeapons.Length; i++)
            {
                //Check if in bounds
                if (CheckInBounds(i, loadoutCompliment.Length))
                {
                    weapons.Add(turrentWeapons[i].SetWeapon(loadoutCompliment[i]));
                }
            }
        }

        private bool CheckInBounds(int index, int length)
        {
            return (index >= 0) && (index < length);
        }
    }
}

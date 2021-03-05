using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.PlayerSystems;

namespace Evacuation.Weapons
{
    public class WeaponSetupHandler
    {
        /// <summary>
        /// Setup the forward weapons with the given forward weapon loadout.
        /// </summary>
        public void SetupForwardWeapons(ref List<IWeapon> weapons, List<string> forwardWeapon, LoadoutHolder[] holders)
        {
            for (int i = 0; i < holders.Length; i++)
            {
                //Check if in bounds
                if (CheckInBounds(i, forwardWeapon.Count))
                {
                    //holders[i].SetWeapon(forwardWeapon[i])
                    weapons.Add(holders[i].SetWeapon(forwardWeapon[i]));
                }
            }
        }

        /// <summary>
        /// Setup the turrent weapons with the given turrent weapon loadout.
        /// </summary>
        public void SetupTurrentWeapons(ref List<IWeapon> weapons, List<string> turrentWeapon, LoadoutHolder[] holders)
        {
            for (int i = 0; i < holders.Length; i++)
            {
                //Check if in bounds
                if (CheckInBounds(i, turrentWeapon.Count))
                    weapons.Add(holders[i].SetWeapon(turrentWeapon[i]));
                Debug.Log("Has been created");
            }
        }

        private bool CheckInBounds(int index, int length)
        {
            return (index >= 0) && (index < length);
        }
    }
}

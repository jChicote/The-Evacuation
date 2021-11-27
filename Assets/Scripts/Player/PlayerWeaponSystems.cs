using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheEvacuation.Character.Weapons;

namespace TheEvacuation.Player.Weapons
{

    public class PlayerWeaponSystems : CharacterWeaponSystem
    {
        // Field

        // Properties

        private void Start()
        {
            pauseInstance = this.GetComponent<IPausable>();
        }

        private void FixedUpdate()
        {
            FireWeapon();
        }
        
        public override void FireWeapon()
        {
            print("Player Firing Weapon");
        }
    }
}

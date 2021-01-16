using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Turrent : Weapon
    {

        public override void InitialiseWeapon(WeaponData data, GameObject projectileShell)
        {
            this.weaponData = data;
            this.projectileShell = projectileShell;

            Debug.Log("has Initialised");
        }

        public override void FireWeapon()
        {
            if (isPaused) return;

            if (timeTillNextFire <= 0)
            {
                Debug.Log("Fired Turrent");
                IProjectile projectile = Instantiate(projectileShell, firingPoint.position, firingPoint.rotation).GetComponent<IProjectile>();
                projectile.InitialiseProjectile(weaponData.speed + 5, weaponData.damage, weaponData.life); //TODO: Include ship speed when firing
                timeTillNextFire = weaponData.fireRate;
            } else
            {
                timeTillNextFire -= Time.fixedDeltaTime;
                Debug.Log("Has been called");
            }
            
        }
    }
}

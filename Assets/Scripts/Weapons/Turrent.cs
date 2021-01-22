using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Turrent : ProjectileWeapon
    {
        /// <summary>
        /// Initialises the weapon on start.
        /// </summary>
        /// <param name="data"></param>
        public override void InitialiseWeapon(WeaponInfo data)
        {
            this.weaponData = data;
        }

        /// <summary>
        /// Turrent weapon fires in succession with increasing counts increasing spread.
        /// </summary>
        public override void FireWeapon()
        {
            if (isPaused) return;

            if (timeTillNextFire <= 0)
            {
                IProjectile projectile = Instantiate(projectileShell, firingPoint.position, firingPoint.rotation).GetComponent<IProjectile>();
                projectile.InitialiseProjectile(weaponData.speed + 5, weaponData.damage, weaponData.life); //TODO: Include ship speed when firing
                timeTillNextFire = weaponData.fireRate;
            } else
            {
                timeTillNextFire -= Time.fixedDeltaTime;
            }
            
        }
    }
}

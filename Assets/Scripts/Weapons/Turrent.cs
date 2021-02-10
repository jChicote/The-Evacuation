using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Turrent : ProjectileWeapon
    {
        private IMovementAccessors movementAccessors;

        /// <summary>
        /// Initialises the weapon on start.
        /// </summary>
        public override void InitialiseWeapon(WeaponInfo data, IMovementAccessors movementAccessors)
        {
            this.weaponData = data;
            this.movementAccessors = movementAccessors;
            Debug.Log(movementAccessors);
        }

        /// <summary>
        /// Turrent weapon fires in succession with increasing counts increasing spread.
        /// </summary>
        public override void FireWeapon(LoadoutPosition currentLoadoutPosition)
        {
            if (isPaused) return;
            if (loadoutPositionType != currentLoadoutPosition) return;

            if (timeTillNextFire <= 0)
            {
                IProjectile projectile = Instantiate(projectileShell, firingPoint.position, firingPoint.rotation).GetComponent<IProjectile>();
                projectile.InitialiseProjectile(weaponData.speed + movementAccessors.CurrentShipSpeed, weaponData.damage, weaponData.life); //TODO: Include ship speed when firing
                timeTillNextFire = weaponData.fireRate;
            } else
            {
                timeTillNextFire -= Time.fixedDeltaTime;
            }
            
        }
    }
}

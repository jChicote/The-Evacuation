using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Actor;

namespace Evacuation.Weapons
{
    public class Turrent : ProjectileWeapon
    {
        private IEntitySpeed movementAccessors;

        /// <summary>
        /// Initialises the weapon on start.
        /// </summary>
        public override void InitialiseWeapon(WeaponInfo data, IEntitySpeed movementAccessors)
        {
            this.weaponData = data;
            this.movementAccessors = movementAccessors;
            this.weaponTransform = this.transform;
            this.tag = transform.root.tag;
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
                projectile.InitialiseProjectile(weaponData.speed + movementAccessors.CurrentShipSpeed, weaponData.damage, weaponData.life); //TODO: Include ship speed when firing
                projectile.SetTag(gameObject.tag);
                timeTillNextFire = weaponData.fireRate;
            } else
            {
                timeTillNextFire -= Time.fixedDeltaTime;
            }
        }

        public override void ProvidePointerLocation(Vector2 pointerPosition)
        {
            lastPointedPosition = pointerPosition;
        }
    }
}

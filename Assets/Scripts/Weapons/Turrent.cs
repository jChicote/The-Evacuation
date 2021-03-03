using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerSystems;

namespace Weapons
{
    public class Turrent : ProjectileWeapon
    {
        private IMovementAccessors movementAccessors;
        private Transform weaponTransform;

        /// <summary>
        /// Initialises the weapon on start.
        /// </summary>
        public override void InitialiseWeapon(WeaponInfo data, IMovementAccessors movementAccessors)
        {
            this.weaponData = data;
            this.movementAccessors = movementAccessors;
            this.weaponTransform = this.transform;
        }

        public override void RotateWeaponToPosition(LoadoutPosition currentLoadoutPosition)
        {
            if (loadoutPositionType != currentLoadoutPosition) return;

            Vector3 positionRelative = lastPointedPosition - transform.position;
            float pointedAngle = Mathf.Atan2(positionRelative.y, positionRelative.x) * Mathf.Rad2Deg - 90;
            Debug.Log(pointedAngle);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, pointedAngle));
            //Vector3 newDir = Vector3.RotateTowards(transform.up, lastPointedPosition - transform.position, Time.deltaTime, 0);

            //Rotates at the lower left corner as center
            //Debug.Log(lastPointedPosition - transform.position);
            //transform.rotation = Quaternion.LookRotation(Vector3.forward, lastPointedPosition - transform.position);
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

        public override void ProvidePointerLocation(Vector2 pointerPosition)
        {
            // Rotates the weapon on it's local position to the direction of the pointer input.

            lastPointedPosition = Camera.main.ScreenToWorldPoint(pointerPosition);
        }
    }
}

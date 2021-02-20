using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerSystems;

namespace Weapons
{
    public class ProjectileWeapon : Weapon
    {
        [Header("Projectile")]
        public GameObject projectileShell;

        public override void FireWeapon(LoadoutPosition currentLoadoutPosition) {}

        public override void InitialiseWeapon(WeaponInfo data, IMovementAccessors movementAccessors) {}

    }
}

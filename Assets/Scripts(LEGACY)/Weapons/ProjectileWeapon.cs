using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Actor;

namespace Evacuation.Weapons
{
    public class ProjectileWeapon : Weapon
    {
        [Header("Projectile")]
        public GameObject projectileShell;

        public override void FireWeapon() {}

        public override void InitialiseWeapon(WeaponInfo data, IEntitySpeed movementAccessors) {}

    }
}

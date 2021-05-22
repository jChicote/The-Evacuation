using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Weapons;

namespace Evacuation.Actor.EnemySystems.DroidSystems
{
    public class DroidSentryWeaponController : EnemyWeaponController
    {
        IWeaponRotator[] weaponRotator;

        // Inspector Accessible Fields
        [SerializeField] private float firingRange = 5;
        [SerializeField] private float firingInterval = 3;
        [SerializeField] private WeaponInfo weaponData; //TEST DATA

        // Interfaces
        private IEnemyTargetingSystem targetingSystem;

        public override void InitialiseWeaponController()
        {
            targetingSystem = this.GetComponent<IEnemyTargetingSystem>();
            weapons = this.GetComponentsInChildren<IWeapon>();
            weaponRotator = this.GetComponentsInChildren<IWeaponRotator>();

            InitialiseWeapons();
            CollectEnemyWeapons();
        }

        private void RotateWeapons()
        {
            foreach (IWeaponRotator weapon in weaponRotator)
            {
                weapon.ProvidePointerLocation(targetingSystem.GetTargetTransform().position);
                weapon.RotateWeaponToDirection();
            }
        }

        private void CollectEnemyWeapons()
        {
            weapons = this.GetComponentsInChildren<IWeapon>();
        }

        private void InitialiseWeapons()
        {
            if (weapons.Length == 0) return;

            IEntitySpeed entitytSpeed = this.GetComponent<IEntitySpeed>();
            foreach (IWeapon weapon in weapons)
            {
                // TODO: data needs to be implemented fgor the controller
                // TODO: movement accessors needs to be changed for global recognition;

               weapon.InitialiseWeapon(weaponData, entitytSpeed);
               weapon.ConfigureWeaponPositioning(LoadoutConfiguration.Pivot);
            }
        }

        private void FireAtTarget()
        {
            foreach (IWeapon weapon in weapons)
            {
                weapon.FireWeapon();
            }
        }

        public override void RunWeaponSystem()
        {
            if (isPaused) return;
            if (targetingSystem.GetDistanceToTarget() > firingRange) return;

            RotateWeapons();
            FireAtTarget();
        }
    }
}

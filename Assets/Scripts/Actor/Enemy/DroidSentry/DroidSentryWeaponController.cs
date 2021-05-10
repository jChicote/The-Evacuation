using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Actor.EnemySystems.DroidSystems
{
    public class DroidSentryWeaponController : EnemyWeaponController
    {
        // Inspector Accessible Fields
        [SerializeField] private float firingRange = 5;
        [SerializeField] private float firingInterval = 3;

        // Interfaces
        private IEnemyTargetingSystem targetingSystem;

        // Fields
        private SimpleTimer simpleTimer;

        public override void InitialiseWeaponController()
        {
            targetingSystem = this.GetComponent<IEnemyTargetingSystem>();
            simpleTimer = new SimpleTimer(firingInterval, Time.deltaTime);

            CollectEnemyWeapons();
        }

        private void CollectEnemyWeapons()
        {
            weapons = this.GetComponentsInChildren<IWeapon>();
        }

        private void InitialiseWeapons()
        {
            if (weapons.Length == 0) return;
            
            foreach (IWeapon weapon in weapons)
            {
                // TODO: data needs to be implemented fgor the controller
                // TODO: movement accessors needs to be changed for global recognition;

               // weapon.InitialiseWeapon();
            }
        }

        private void FireAtTarget()
        {
            print("Is Firing");
        }

        public override void RunWeaponSystem()
        {
            if (isPaused) return;
            if (targetingSystem.GetDistanceToTarget() > firingRange) return;

            simpleTimer.TickTimer();
            if(simpleTimer.CheckTimeIsUp())
            {
                FireAtTarget();
                simpleTimer.ResetTimer();
            }
        }
    }
}

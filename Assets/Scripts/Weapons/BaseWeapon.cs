using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheEvacuation.TimeUtility;

namespace TheEvacuation.Weapon
{
    public class BaseWeapon : BaseInteractiveObject
    {
        // Fields
        public GameObject projectile;
        public SimpleTimer timer;

        private bool canRotate = false;

        // Properties
        public bool CanRotate { get => canRotate; set => canRotate = value; }

        private void Start()
        {
            timer = new SimpleTimer(0.5f, Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (IsPaused) return;

            if (timer.CheckTimeIsUp())
            {
                Fire();
                timer.ResetTimer();
            }

            timer.TickTimer();
        }

        public virtual void Fire()
        {
            print("Weapon Has Fired");
        }

        public virtual void RotateTowardsAimPosition(Vector2 aimTargetPoint)
        {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheEvacuation.TimeUtility;

namespace TheEvacuation.Weapon
{
    public interface IWeapon
    {
        Vector2 MousePosition { get; set; }
        void Fire();
    }

    public interface IWeaponRotator
    {
        bool CanRotate { get; set; }
    }

    public class BaseWeapon : BaseInteractiveObject, IWeapon, IWeaponRotator
    {
        // Fields
        public GameObject projectile;
        public TheEvacuation.TimeUtility.SimpleTimer timer;
        private Vector2 mousePosition;

        private bool canRotate = false;

        // Properties
        public Vector2 MousePosition { get => mousePosition; set => mousePosition = value; }
        public bool CanRotate { get => canRotate; set => canRotate = value; }

        private void Start()
        {
            timer = new TimeUtility.SimpleTimer(0.5f, Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (IsPaused) return;

            RotateTowardsAimPosition(mousePosition);
        }

        public virtual void Fire()
        {
            print("Firing Weapon");

            if (timer.CheckTimeIsUp())
            {
                Instantiate(projectile, transform.position, transform.rotation);
                timer.ResetTimer();
            }

            timer.TickTimer();
        }

        public virtual void RotateTowardsAimPosition(Vector2 aimTargetPoint)
        {
            if (!canRotate) return;
        }
    }
}

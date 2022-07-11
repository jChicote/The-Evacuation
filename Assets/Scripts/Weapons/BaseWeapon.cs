using TheEvacuation.Common;
using UnityEngine;

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
        public GameObject projectile;
        public TimeUtility.SimpleCountDown timer;
        protected Vector2 mousePosition;
        protected Vector2 relativeWeaponDirectionRotation;

        private bool canRotate = false;
        private float weaponRotAngle;

        public Vector2 MousePosition { get => mousePosition; set => mousePosition = value; }
        public bool CanRotate { get => canRotate; set => canRotate = value; }

        private void Start()
        {
            timer = new TimeUtility.SimpleCountDown(0.5f, Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (IsPaused) return;

            RotateTowardsAimPosition(mousePosition);
        }

        public virtual void Fire()
        {
            // print("Firing Weapon");

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

            relativeWeaponDirectionRotation = Camera.main.ScreenToWorldPoint(mousePosition) - transform.position;
            weaponRotAngle = Mathf.Atan2(relativeWeaponDirectionRotation.y, relativeWeaponDirectionRotation.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Euler(0, 0, weaponRotAngle);
        }
    }
}

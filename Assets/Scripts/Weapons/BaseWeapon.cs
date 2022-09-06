using TheEvacuation.Common;
using UnityEngine;

namespace TheEvacuation.Weapon
{

    public interface IWeapon
    {

        #region - - - - - - Methods - - - - - -

        Vector2 MousePosition { get; set; }
        void Fire();

        #endregion Methods

    }

    public interface IWeaponRotator
    {

        #region - - - - - - Methods - - - - - -

        bool CanRotate { get; set; }

        #endregion Methods

    }

    public class BaseWeapon : BaseInteractiveObject, IWeapon, IWeaponRotator
    {

        #region - - - - - - Fields - - - - - -

        public GameObject projectile;
        public TimeUtility.SimpleCountDown timer;

        protected Vector2 mousePosition;
        protected Vector2 relativeWeaponDirectionRotation;

        private bool canRotate = false;
        private float weaponRotAngle;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public Vector2 MousePosition { get => mousePosition; set => mousePosition = value; }

        public bool CanRotate { get => canRotate; set => canRotate = value; }

        #endregion Properties

        #region - - - - - - MonoBehaviour - - - - - -

        private void Start()
            => timer = new TimeUtility.SimpleCountDown(0.5f, Time.deltaTime);

        private void FixedUpdate()
        {
            if (IsPaused) return;

            RotateTowardsAimPosition(mousePosition);
        }

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - - -

        public virtual void Fire()
        {
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

        #endregion Methods

    }

}

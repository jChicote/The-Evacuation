using TheEvacuation.Common;
using UnityEngine;

namespace TheEvacuation.PlayerSystems.Movement
{

    public class ShipMovementSystem : GameHandler, ICharacterMovement
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField]
        protected Rigidbody2D characterRB;
        protected Vector2 currentVelocity = Vector2.zero;
        protected Vector2 currentDirection = Vector2.zero;
        protected Vector2 projectedVelocity = Vector2.zero;

        protected IPausable pauseInstance;

        [SerializeField]
        protected float angleRotation;
        protected float currentSpeed = 3; // Controls speed
        protected bool isMovementKeyHeld = false;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public bool IsMovementHeld { get => isMovementKeyHeld; set => isMovementKeyHeld = value; }

        #endregion Properties

        #region - - - - - - MonoBehaviour - - - - - -

        private void Start()
        {
            pauseInstance = this.GetComponent<IPausable>();
        }

        private void Update()
        {
            if (pauseInstance.IsPaused) return;

            UpdateMovement();
            UpdateRotation();
        }

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - - -

        public override object AwakeHandle(object request)
        {
            // Future: uses dto to get all data from the scene model layer

            return base.AwakeHandle(request);
        }

        protected virtual void UpdateMovement()
        {
            if (isMovementKeyHeld)
                currentVelocity = Vector2.Lerp(currentVelocity, projectedVelocity, 0.07f);
            else
                currentVelocity = Vector2.Lerp(currentVelocity, Vector3.zero, 0.03f);

            characterRB.velocity = currentVelocity;
        }

        protected virtual void UpdateRotation()
            => transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleRotation));

        public void CalculateMovement(Vector2 normalisedDirection)
            => projectedVelocity = normalisedDirection * currentSpeed;

        public void CalculateShipRotation(Vector2 startPos, Vector2 endPos)
        {
            currentDirection = (endPos - startPos).normalized;
            angleRotation = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg - 90;
        }

        #endregion Methods

    }

}
using TheEvacuation.Shared;
using UnityEngine;

namespace TheEvacuation.Player.Movement
{

    public class ShipMovementSystem : MonoBehaviour, ICharacterMovement
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
        protected float currentSpeed = 0;
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

        protected virtual void UpdateMovement()
        {
            if (!isMovementKeyHeld)
                currentVelocity = Vector2.Lerp(currentVelocity, Vector3.zero, 0.03f);
            else
                currentVelocity = Vector2.Lerp(currentVelocity, projectedVelocity, 0.07f);

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

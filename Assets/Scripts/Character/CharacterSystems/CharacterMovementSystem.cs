using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheEvacuation.Character.Movement
{
    public interface ICharacterMovement
    {
        bool IsMovementHeld { get; set; }

        void CalculateMovement(Vector2 normalisedDirection);
        void CalculateShipRotation(Vector2 startPos, Vector2 endPos);
    }

    public class CharacterMovementSystem : MonoBehaviour, ICharacterMovement
    {
        // Fields
        [SerializeField]
        protected Rigidbody2D characterRB;
        protected Vector2 projectedVelocity = Vector2.zero;
        protected Vector2 currentVelocity = Vector2.zero;
        protected Vector2 currentDirection = Vector2.zero;

        protected IPausable pauseInstance;

        [SerializeField]
        protected float currentSpeed = 0;
        protected float angleRotation;
        protected bool isMovementKeyHeld = false;

        // Properties
        public bool IsMovementHeld { get => isMovementKeyHeld; set => isMovementKeyHeld = value; }

        // Start is called before the first frame update
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

        protected virtual void UpdateMovement()
        {
            if (!isMovementKeyHeld)
                currentVelocity = Vector2.Lerp(currentVelocity, Vector3.zero, 0.03f);
            else
                currentVelocity = Vector2.Lerp(currentVelocity, projectedVelocity, 0.07f);

            characterRB.velocity = currentVelocity;
        }

        protected virtual void UpdateRotation()
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleRotation));
        }

        public void CalculateMovement(Vector2 normalisedDirection)
        {
            projectedVelocity = normalisedDirection * currentSpeed;
        }

        public void CalculateShipRotation(Vector2 startPos, Vector2 endPos)
        {
            currentDirection = (endPos - startPos).normalized;
            angleRotation = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg - 90;
        }
    }
}

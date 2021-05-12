using UnityEngine;

// Summary:
//      Generalised accessor for accessing class private variables
public interface IEntitySpeed
{
    float CurrentShipSpeed { get; }
}

namespace Evacuation.Actor.PlayerSystems
{
    // Summary:
    //      This defines the input-system realted interface for reference to vector positions on inputs.
    public interface IMovement
    {
        void InitialiseMovement();
        void CalculateMovement(Vector2 startPos, Vector2 currentPos);
        void CalculateLocalRotation(Vector2 centerPos, Vector2 currentPos);
        void SetTriggerIsHeld(bool isHeld);
    }

    // Summary:
    //      This defines the player's landing actions needed when captured by a platform.
    public interface IAutoLandingManoeuvre
    {
        void AutoLand(Transform landingPosition);
        void TakeOff();
        bool CheckHasLanded();
        void LockMovement();
        void UnlockMovement();
    }

    // Summary:
    //      This just gets the ship's position globally
    public interface IShipPositionLocator
    {
        Vector2 GetShipPosition();
    }

    // Summary:
    //      The player movement controller handles all the movmenet and rotation related
    //      calculations and transformation of the player during gameplay.
    public class PlayerMovementController : MonoBehaviour, IMovement, IEntitySpeed, IAutoLandingManoeuvre, IShipPositionLocator
    {
        // Interfaces 
        private ICheckPaused pauseChecker;

        // Fields
        private Rigidbody2D playerRB;
        private Vector2 targetVelocity = Vector2.zero;
        private Vector2 currentVelocity = Vector2.zero;
        private Vector2 currentDirection = Vector2.zero;
        private ShipInfo shipInfo;

        private float currentSpeed;
        private float angleRotation;
        private bool hasLanded = false;
        private bool isMovementLocked = false;
        private bool isTriggerHeld = false;

        //private float maxRadiusTransform = 450f / 2; //DEFAULT VALUES FROM UI Joystick

        // Accessors
        float IEntitySpeed.CurrentShipSpeed
        {
            get { return currentSpeed; }
        }

        public void InitialiseMovement()
        {
            pauseChecker = this.GetComponent<ICheckPaused>();
            playerRB = this.GetComponent<Rigidbody2D>();

            this.shipInfo = this.GetComponent<IPlayerStats>().GetShipStats();
            currentSpeed = shipInfo.maxSpeed; 
        }

        private void FixedUpdate()
        {
            if (pauseChecker.CheckIsPaused())
            {
                playerRB.velocity = Vector2.zero;
                return;
            }

            if (!ValidateRequirements()) return;
            if (isMovementLocked) return;

            RunMovement();
            RunRotation();
        }

        public void CalculateMovement(Vector2 startPos, Vector2 currentPos)
        {
            if (!isTriggerHeld) return;

            //currentSpeed = shipInfo.maxSpeed * (Vector3.Magnitude(startPos - currentPos) / maxRadiusTransform);
            currentSpeed = shipInfo.maxSpeed;

            targetVelocity = currentPos * currentSpeed;
        }

        public void CalculateLocalRotation(Vector2 centerPos, Vector2 currentPos)
        {
            currentDirection = (currentPos - centerPos).normalized;
            angleRotation = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg - 90;
        }

        private void RunMovement()
        {
            if (!isTriggerHeld)
                currentVelocity = Vector2.Lerp(currentVelocity, Vector3.zero, 0.03f);
            else
                currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, 0.07f);

            playerRB.velocity = currentVelocity;
        }

        private void RunRotation()
        {
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleRotation));
        }

        /// <summary>
        /// Checks crucial variables ensuring that they exist before running functions.
        /// </summary>
        private bool ValidateRequirements()
        {
            if (playerRB == null) return false;

            return true;
        }

        /// <summary>
        /// Called to land the vessel autonomously whilst controls are locked out.
        /// </summary>
        public void AutoLand(Transform landingPosition)
        {
            playerRB.velocity = Vector2.zero;
            transform.position = Vector3.MoveTowards(transform.position, landingPosition.position, 0.05f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, landingPosition.rotation, 6f);
            hasLanded = transform.position == landingPosition.position && transform.rotation == landingPosition.rotation;
        }

        public void SetTriggerIsHeld(bool isHeld)
        {
            isTriggerHeld = isHeld;
        }

        public Vector2 GetShipPosition()
        {
            return transform.position;
        }

        public bool CheckHasLanded()
        {
            return hasLanded;
        }

        public void LockMovement()
        {
            isMovementLocked = true;
        }

        public void UnlockMovement()
        {
            isMovementLocked = false;
        }

        public void TakeOff()
        {
            isMovementLocked = false;
        }
    }
}
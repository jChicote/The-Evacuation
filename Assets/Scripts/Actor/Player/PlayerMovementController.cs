using UnityEngine;


namespace PlayerSystems
{
    // Summary:
    //      This defines the input-system realted interface for reference to vector positions on inputs.
    public interface IMovement
    {
        void InitialiseMovement();
        void CalculateMovement(Vector2 startPos, Vector2 currentPos);
    }

    // Summary:
    //      Generalised accessor for accessing class private variables
    public interface IMovementAccessors
    {
        float CurrentShipSpeed { get; }
    }

    // Summary:
    //      This defines the player's landing actions needed when captured by a platform.
    public interface IAutoLandingManoeuvre
    {
        void AutoLand(Transform landingPosition);
        void TakeOff();
        bool CheckHasLanded();
        void LockMovement();
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
    public class PlayerMovementController : MonoBehaviour, IMovement, IMovementAccessors, IAutoLandingManoeuvre, IShipPositionLocator
    {
        // Interfaces 
        private ICheckPaused pauseChecker;

        // Fields
        private Rigidbody2D playerRB;
        private Vector3 currentVelocity;
        private Vector3 currentDirection;
        private ShipInfo shipInfo;

        private float currentSpeed;
        private float angleRotation;
        private bool hasLanded = false;
        private bool isMovementLocked = false;

        private float maxRadiusTransform = 450f / 2; //DEFAULT VALUES FROM UI Joystick

        // Accessors
        float IMovementAccessors.CurrentShipSpeed
        {
            get { return currentSpeed; }
        }

        public void InitialiseMovement()
        {
            pauseChecker = this.GetComponent<ICheckPaused>();
            playerRB = this.GetComponent<Rigidbody2D>();

            this.shipInfo = this.GetComponent<IShipData>().GetShipStats();
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

        /// <summary>
        /// Calcualtes the movement of the player.
        /// </summary>
        public void CalculateMovement(Vector2 startPos, Vector2 currentPos)
        {
            currentSpeed = shipInfo.maxSpeed * (Vector3.Magnitude(startPos - currentPos) / maxRadiusTransform);
            currentDirection = (currentPos - startPos).normalized;
            currentVelocity = currentDirection * currentSpeed;

            CalculateRotation();
        }

        /// <summary>
        /// Calculates the local rotation of the player.
        /// </summary>
        private void CalculateRotation()
        {
            angleRotation = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg - 90;
        }

        /// <summary>
        /// Performs the movement actions.
        /// </summary>
        private void RunMovement()
        {
            playerRB.velocity = currentVelocity;
        }

        /// <summary>
        /// Performs the player's rotation.
        /// </summary>
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

            if (transform.position == landingPosition.position && transform.rotation == landingPosition.rotation)
            {
                hasLanded = true;
            } else
            {
                hasLanded = false;
            }
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

        public void TakeOff()
        {
            isMovementLocked = false;
        }
    }
}
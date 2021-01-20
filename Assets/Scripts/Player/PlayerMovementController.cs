using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    void InitialiseMovement();
    void CalculateMovement(Vector2 startPos, Vector2 currentPos);
}

public class PlayerMovementController : MonoBehaviour, IMovement
{
    // Interfaces 
    private ICheckPaused pauseChecker;

    // Physics and Motion
    public Rigidbody2D playerRB;
    public float currentSpeed;
    public float angleRotation;
    public Vector3 currentVelocity;
    public Vector3 currentDirection;

    // Stats, Modifiers and other localised variables
    public ShipStats shipStats;

    [Header("Test Variables")]
    private float maxRadiusTransform = 450f / 2; //DEFAULT VALUES FROM UI

    public void InitialiseMovement()
    {
        pauseChecker = this.GetComponent<ICheckPaused>();
        playerRB = this.GetComponent<Rigidbody2D>();

        this.shipStats = this.GetComponent<IShipData>().GetShipStats();
    }

    private void FixedUpdate()
    {
        if (pauseChecker.CheckIsPaused())
        {
            playerRB.velocity = Vector2.zero;
            return;
        }
        if(!ValidateRequirements()) return;

        RunMovement();
        RunRotation();
    }

    /// <summary>
    /// Calcualtes the movement of the player.
    /// </summary>
    public void CalculateMovement(Vector2 startPos, Vector2 currentPos)
    {
        currentSpeed = shipStats.maxSpeed * (Vector3.Magnitude(startPos - currentPos) / maxRadiusTransform);
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
}

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
    private ICheckPaused pauseChecker;

    public Rigidbody2D playerRB;
    public float currentSpeed;
    public Vector3 currentVelocity;
    public Vector3 currentDirection;

    [Header("Test Variables")]
    public float maxVelocity = 5f;
    private float maxRadiusTransform = 450f / 2; //DEFAULT VALUES FROM UI

    public void InitialiseMovement()
    {
        pauseChecker = this.GetComponent<ICheckPaused>();
        playerRB = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (pauseChecker.CheckIsPaused()) return;

        RunMovement();
    }

    /// <summary>
    /// 
    /// </summary>
    public void CalculateMovement(Vector2 startPos, Vector2 currentPos)
    {
        Debug.Log("Is calculating movement");
        currentSpeed = maxVelocity * (Vector3.Magnitude(startPos - currentPos) / maxRadiusTransform);
        currentDirection = (currentPos - startPos).normalized;
        currentVelocity = currentDirection * currentSpeed;
    }

    /// <summary>
    /// 
    /// </summary>
    private void RunMovement()
    {
        playerRB.velocity = currentVelocity;
    }

    /// <summary>
    /// 
    /// </summary>
    private bool ValidateRequirements()
    {
        if (playerRB == null) return false;

        return true;
    }
}

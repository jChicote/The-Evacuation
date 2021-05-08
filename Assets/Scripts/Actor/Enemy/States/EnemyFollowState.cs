using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Actor.EnemySystems
{
    /// <summary>
    /// This state enables for enemies to follow target smoothly tracking in the direction
    /// of the player's position. This is suitable when the player is undocked from a 
    /// capture platform.
    /// </summary>
    public class EnemyFollowState : BaseComponentState
    {
        // Interfaces
        protected IEnemyTargetingSystem targetingSystem;
        protected IStateManager stateManager;
        protected IMovementController movementController;

        // Fields
        protected Rigidbody2D enemyRB;
        protected Transform shipTransform;
        protected Vector2 shipDirection;
        protected float shipSpeed;

        [SerializeField] private float detectionDist = 6;

        public override void BeginState()
        {
            shipTransform = transform;
            enemyRB = this.GetComponent<Rigidbody2D>();
            targetingSystem = this.GetComponent<IEnemyTargetingSystem>();
            stateManager = this.GetComponent<IStateManager>();
            movementController = this.GetComponent<IMovementController>();
            targetingSystem.SelectNearestTarget();
        }

        private void FixedUpdate()
        {
            if (isPaused) return;

            RunStateUpdate();
        }

        private void SwitchToOrbitalMovement()
        {
            if (stateManager == null) return;

            // Checks whether the distance to target is within the detection radius
            if (Vector2.Distance(targetingSystem.GetTargetTransform().position, shipTransform.position) < detectionDist)
            {
                stateManager.AddState<EnemyOrbitState>();
            }
        }

        protected virtual void CalculateDirection()
        {
            shipDirection = (Vector2)targetingSystem.GetTargetTransform().position - (Vector2)shipTransform.position;
            
            //angleRotation = Mathf.Atan2(shipDirection.y, shipDirection.x) * Mathf.Rad2Deg - 90;
        }

        protected virtual void CalculateSpeed()
        {
            // Test speed for now
            shipSpeed = 7f;
        }

        public override void RunStateUpdate()
        {
            CalculateDirection();
            CalculateSpeed();
            movementController.SetMovement(shipDirection.normalized * shipSpeed);

            SwitchToOrbitalMovement();
        }
    }
}

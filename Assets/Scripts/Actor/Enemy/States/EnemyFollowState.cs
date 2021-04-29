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

        // Fields
        protected Rigidbody2D enemyRB;
        protected Transform shipTransform;
        protected Vector2 shipDirection;
        protected float shipSpeed;

        [SerializeField] private float orbitDistance = 5;

        public override void BeginState()
        {
            shipTransform = transform;
            enemyRB = this.GetComponent<Rigidbody2D>();
            targetingSystem = this.GetComponent<IEnemyTargetingSystem>();
            stateManager = this.GetComponent<IStateManager>();
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
            if (Vector3.Distance(targetingSystem.GetTargetTransform().position, shipTransform.position) < 8) return;

            stateManager.AddState<EnemyOrbitState>();
        }

        protected virtual void CalculateDirection()
        {
            shipDirection = targetingSystem.GetTargetTransform().position - shipTransform.position;
            //angleRotation = Mathf.Atan2(shipDirection.y, shipDirection.x) * Mathf.Rad2Deg - 90;
        }

        protected virtual void CalculateSpeed()
        {
            // Test speed for now
            shipSpeed = 7f;
        }

        protected virtual void SetMovement()
        {
            enemyRB.velocity = shipDirection.normalized * shipSpeed;
            //shipTransform.position = new Vector3(shipTransform.position.x, shipTransform.position.y, 0);
        }


        public override void RunStateUpdate()
        {
            CalculateDirection();
            CalculateSpeed();
            SetMovement();

            SwitchToOrbitalMovement();
        }
    }
}

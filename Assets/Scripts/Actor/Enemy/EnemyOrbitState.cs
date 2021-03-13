using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Actor.EnemySystems
{
    public class EnemyOrbitState : BaseComponentState
    {
        // Interfaces
        protected IEnemyTargetingSystem targetingSystem;
        protected IStateManager stateManager;

        // Fields
        protected Rigidbody2D enemyRB;
        protected Transform shipTransform;
        protected Vector2 shipDirection;
        private Vector2 shipToTargetVelocity;
        private Vector2 shipForce;
        protected float shipSpeed;
        private float shipOrbitalForce;
        private float radiusToTarget = 9;

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

        private void SwitchToFollowState()
        {
            if (stateManager == null) return;
            if (Vector3.Distance(targetingSystem.GetTargetTransform().position, shipTransform.position) < 10) return;

            stateManager.AddState<EnemyFollowState>();
        }

        protected virtual void CalculateRepelVelocity()
        {
            //shipToTargetVelocity = targetingSystem.GetTargetTransform().position - shipTransform.position;
            //shipToTargetVelocity *= 2;
            // shipOrbitalForce = Mathf.Sqrt((2 * 23) / radiusToTarget);
            //shipForce = shipTransform.position.normalized * shipOrbitalForce;
            //angleRotation = Mathf.Atan2(shipDirection.y, shipDirection.x) * Mathf.Rad2Deg - 90;

            shipTransform.RotateAround(targetingSystem.GetTargetTransform().position, Vector3.forward, 100 * Time.unscaledDeltaTime);

        }

        protected virtual void CalculateSpeed()
        {
            // Test speed for now
            shipSpeed = 7f;
        }

        protected virtual void SetMovement()
        {
            Debug.Log(shipForce);
            enemyRB.velocity = shipDirection.normalized * shipSpeed;
        }


        public override void RunStateUpdate()
        {
            CalculateRepelVelocity();
            CalculateSpeed();
            SetMovement();

            SwitchToFollowState();
        }
    }
}
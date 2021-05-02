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
        protected Vector2 shipVelocity;
        private Vector2 pointA;
        private Vector2 pointB;
        private Vector2 radialVelocity;
        private Vector2 drivingDirection;
        private float abMagnitude;
        protected float shipSpeed;

        // Inspector Accessible Fields
        [SerializeField] private float radialOrbitDist = 7f;
        [SerializeField] private float exclusionDist = 10f;

        Vector2 radialPoint = Vector2.zero;

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

            // Checks whether the distance to target is outside the exclusion perimeter of the target
            if (Vector2.Distance(targetingSystem.GetTargetTransform().position, shipTransform.position) > exclusionDist)
            {
                stateManager.AddState<EnemyFollowState>();
            }

            
        }

        protected virtual void CalculateOrbitalVelocity()
        {
            //print(shipTransform.position.z);
            pointA = (Vector2)shipTransform.position;
            pointB = (Vector2)targetingSystem.GetTargetTransform().position;
            abMagnitude = Vector3.Magnitude(pointA - pointB);
            radialPoint = new Vector2(pointB.x + (radialOrbitDist * (pointA.y - pointB.y)) / abMagnitude, pointB.y + (radialOrbitDist * (pointB.x - pointA.x)) / abMagnitude);
            radialVelocity = (shipTransform.position - targetingSystem.GetTargetTransform().position).normalized * 9f;
            drivingDirection = radialPoint - (Vector2)shipTransform.position;

            shipVelocity = (drivingDirection + radialVelocity).normalized * shipSpeed;
            //print(shipTransform.position.z);

            //Debug.DrawRay(targetingSystem.GetTargetTransform().position, radialVelocity);
            //Debug.DrawRay(shipTransform.position, tangentialVelocity, Color.red);
            //Debug.DrawRay(shipTransform.position, shipDirection, Color.red);
            //Debug.DrawRay(shipTransform.position, resultantVelocity, Color.green);
        }

        protected virtual void CalculateSpeed()
        {
            // Test speed for now
            shipSpeed = 7f;
        }

        protected virtual void SetMovement()
        {
            enemyRB.velocity = shipVelocity;
        }


        public override void RunStateUpdate()
        {
            CalculateOrbitalVelocity();
            CalculateSpeed();
            SetMovement();

            SwitchToFollowState();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(targetingSystem.GetTargetTransform().position, exclusionDist);
            //Gizmos.color = Color.blue;
            //Gizmos.DrawWireSphere(radialPoint, 1f);
        }
    }
}
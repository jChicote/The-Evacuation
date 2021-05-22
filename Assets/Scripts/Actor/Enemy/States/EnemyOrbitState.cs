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
        protected IMovementController movementController;
        private IWeaponController weaponController;

        // Fields
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
            targetingSystem = this.GetComponent<IEnemyTargetingSystem>();
            weaponController = this.GetComponent<IWeaponController>();
            stateManager = this.GetComponent<IStateManager>();
            movementController = this.GetComponent<IMovementController>();
            targetingSystem.SelectNearestTarget();
        }

        private void FixedUpdate()
        {
            if (isPaused) return;
            RunStateUpdate();

            // Weapon Fire
            weaponController.RunWeaponSystem();
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
            pointA = shipTransform.position;
            pointB = targetingSystem.GetTargetTransform().position;
            abMagnitude = Vector3.Magnitude(pointA - pointB);
            radialPoint = new Vector2(pointB.x + (radialOrbitDist * (pointA.y - pointB.y)) / abMagnitude, pointB.y + (radialOrbitDist * (pointB.x - pointA.x)) / abMagnitude);
            radialVelocity = (shipTransform.position - targetingSystem.GetTargetTransform().position).normalized * 9f;
            drivingDirection = radialPoint - (Vector2)shipTransform.position;

            shipVelocity = (drivingDirection + radialVelocity).normalized * shipSpeed;
        }

        protected virtual void CalculateSpeed()
        {
            // Test speed for now
            shipSpeed = 7f;
        }

        public override void RunStateUpdate()
        {
            CalculateOrbitalVelocity();
            CalculateSpeed();
            movementController.SetMovement(shipVelocity);

            SwitchToFollowState();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(targetingSystem.GetTargetTransform().position, exclusionDist);
        }
    }
}
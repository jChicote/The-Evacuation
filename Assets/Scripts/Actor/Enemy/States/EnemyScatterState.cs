using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Actor.EnemySystems
{
    public class EnemyScatterState : BaseComponentState
    {
        // Interfaces
        protected IEnemyTargetingSystem targetingSystem;
        protected IStateManager stateManager;
        protected IMovementController movementController;
        protected IWeaponController weaponController;

        // Fields
        private Camera mainCamera;
        protected Transform shipTransform;
        private SimpleTimer timer;
        private Vector2 shipVelocity = Vector2.zero;
        private Vector2 selectedDestination = Vector2.zero;
        private Vector2 viewPlaneDimensions = Vector2.one;
        private bool canTravel = false;
        private float interpolationValue = 0;

        public override void BeginState()
        {
            mainCamera = Camera.main;
            timer = new SimpleTimer(2f, Time.deltaTime);
            shipTransform = transform;

            movementController = this.GetComponent<IMovementController>();
            weaponController = this.GetComponent<IWeaponController>();
            targetingSystem = this.GetComponent<IEnemyTargetingSystem>();
            stateManager = this.GetComponent<IStateManager>();

            movementController.SetMovement(Vector2.zero);
            targetingSystem.SelectNearestTarget();
        }

        private void FixedUpdate()
        {
            if (isPaused) return;
            RunStateUpdate();

            // Run weapon fire
            weaponController.RunWeaponSystem();
        }

        public override void RunStateUpdate()
        {
            if (timer.CheckTimeIsUp())
            {
                DetermineAreaBoundaries();
                SelectNewPosition();
                timer.ResetTimer();
                interpolationValue = 0;
                return;
            }

            // keep timer ticking
            timer.TickTimer();
            TranslateToNewLocation();
        }

        private float CalcualteDistanceToDestination()
        {
            return Vector3.Magnitude(shipTransform.position - (Vector3)selectedDestination);
        }

        private void TranslateToNewLocation()
        {
            // Stop movement when within minimum distance
            if (CalcualteDistanceToDestination() < 0.5f)
            {
                movementController.SetPosition(selectedDestination);
                return;
            }

            movementController.SetPosition(Vector2.Lerp(shipTransform.position, selectedDestination, interpolationValue));
            interpolationValue += Time.deltaTime;
            interpolationValue = Mathf.Clamp(interpolationValue, 0, 1);
        }

        private void SelectNewPosition()
        {
            float xDimension = viewPlaneDimensions.x / 2;
            float yDimension = viewPlaneDimensions.y / 2;
            selectedDestination.x = (Random.Range(1, 10) >= 5 ? 1 : -1) * (Random.Range(0, xDimension) + (viewPlaneDimensions.x / 2)) + mainCamera.transform.position.x;
            selectedDestination.y = (Random.Range(1, 10) >= 5 ? 1 : -1) * (Random.Range(0, yDimension) + (viewPlaneDimensions.y / 2))  + mainCamera.transform.position.y;
        }

        /// <summary>
        /// Calculates the different bounds of the map to provide
        /// the proper dimensions of the travel area for the droid
        /// </summary>
        private void DetermineAreaBoundaries()
        {
            viewPlaneDimensions.x = Camera.main.orthographicSize * Camera.main.aspect;
            viewPlaneDimensions.y = viewPlaneDimensions.x * (float)(Screen.height * 1.0 / Screen.width * 1.0);
        }
    }
}

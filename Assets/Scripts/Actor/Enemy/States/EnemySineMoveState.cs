using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Actor.EnemySystems.States
{
    public interface IEnemySineMovementState
    {
        /// <summary>
        /// Decides the movmenet orientation that the entity will take in its wave motion.
        /// </summary>
        /// <param name="waveDirection">Direction of the wave on either Vertical or Horizontal plane</param>
        /// <param name="waveOrientation">Orientation based as either positive one or negative on the plane</param>
        void DecideSineWaveDirection(SineWaveDirection waveDirection, int waveOrientation);
    }
    public class EnemySineMoveState : BaseComponentState, IEnemySineMovementState
    {
        // Interfaces
        protected IMovementController movementController;
        protected IStateManager stateManager;

        // Fields
        private SineWaveDirection waveDirection;
        private Transform shipTransform;
        private Vector2 shipPosition = Vector2.zero;
        private int waveOrientation = 1;

        public override void BeginState()
        {
            shipTransform = this.transform;
            shipPosition = shipTransform.position;

            movementController = this.GetComponent<IMovementController>();
            stateManager = this.GetComponent<IStateManager>();
        }

        public void DecideSineWaveDirection(SineWaveDirection waveDirection, int waveOrientation)
        {
            this.waveDirection = waveDirection;
            this.waveOrientation = waveOrientation;
        }

        private void FixedUpdate()
        {
            if (isPaused) return;

            if (waveDirection == SineWaveDirection.Vertical)
            {
                CalculateVerticalSineMotionPosition();
            } else
            {
                CalculateHorizontalSineMotionPosition();
            }

            ApplyMovement();
        }

        private void CalculateVerticalSineMotionPosition()
        {
            // Use Mathf.Sin() to determine position
        }

        private void CalculateHorizontalSineMotionPosition()
        {
            // Use Mathf.Sin() to determine position
        }

        private void ApplyMovement()
        {
            shipTransform.position = shipPosition;
        }

    }

    public enum SineWaveDirection
    {
        Vertical,
        Horizontal
    }
}

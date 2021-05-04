using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Actor.EnemySystems
{
    public interface IMovementController
    {
        void InitialiseController();
        void SetMovement(Vector2 velocity);
    }

    public class EnemyMovementController : MonoBehaviour, IMovementController, IPausable
    {
        // Interfaces
        private IStateManager stateManager;
        private Rigidbody2D enemyRB;

        // Fields
        private bool isPaused = false;

        public void InitialiseController()
        {
            enemyRB = this.GetComponent<Rigidbody2D>();
        }

        public void SetMovement(Vector2 velocity)
        {
            enemyRB.velocity = velocity;
        }

        public void SetRotation()
        {

        }

        public void StopAllMovement()
        {
            if (isPaused)
            {
                enemyRB.angularVelocity = 0;
                enemyRB.velocity = Vector2.zero;
                return;
            }
        }

        public void OnPause()
        {
            print("Called this");
            isPaused = true;
            StopAllMovement();
        }

        public void OnUnpause()
        {
            isPaused = false;
        }
    }
}

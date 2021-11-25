using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Actor.EnemySystems
{
    public interface IMovementController
    {
        void InitialiseController();
        void SetMovement(Vector2 velocity);
        void SetPosition(Vector2 position);
    }

    public class EnemyMovementController : MonoBehaviour, IMovementController, IPausable, IEntitySpeed
    {
        // Inspector Accessible Fields
        [SerializeField] private float speed = 5; // TEST DATA

        // Interfaces
        private IStateManager stateManager;
        private Rigidbody2D enemyRB;

        // Fields
        private Transform shipTransform;
        private bool isPaused = false;

        public float CurrentShipSpeed => speed;

        public void InitialiseController()
        {
            enemyRB = this.GetComponent<Rigidbody2D>();
            shipTransform = this.transform;
        }

        public void SetMovement(Vector2 velocity)
        {
            enemyRB.velocity = velocity;
        }

        public void SetPosition(Vector2 position)
        {
            shipTransform.position = position;
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

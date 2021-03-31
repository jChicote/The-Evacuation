using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Actor.EnemySystems
{
    public class EnemyMovementController : MonoBehaviour, IPausable
    {
        // Interfaces
        private IStateManager stateManager;

        // Fields
        private bool isPaused = false;

        private void FixedUpdate()
        {
            if (isPaused) return;
        }

        public void OnPause()
        {
            isPaused = true;
        }

        public void OnUnpause()
        {
            isPaused = false;
        }
    }
}

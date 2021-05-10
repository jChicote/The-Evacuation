using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Actor.EnemySystems
{
    public interface IAssignSceneActorTracker
    {
        void SetSceneActorTracker(IActorTracker actorTracker);
    }

    public interface IEnemyTargetingSystem
    {
        void InitialiseTargetingSystem();
        void SelectNearestTarget();
        Transform GetTargetTransform();
        float GetDistanceToTarget();
    }

    public class EnemyTargetingSystem : MonoBehaviour, IAssignSceneActorTracker, IEnemyTargetingSystem
    {
        // Fields
        private Transform targetTransform = null;
        private IActorTracker sceneActorTracker;
        private Transform shipTransform;

        public void InitialiseTargetingSystem()
        {
            shipTransform = transform;
        }

        // Selects the nearest target relative to this posiiton. The system will already 'know' where
        // the players are in the game, and decide on action based on the relative proximity to the player,
        // and the climate of battle in the scene.
        public void SelectNearestTarget()
        {
            targetTransform = sceneActorTracker.GetRandomFriendly().transform;
        }

        public Transform GetTargetTransform() { return targetTransform; }

        public float GetDistanceToTarget()
        {
            if (targetTransform == null) return 1000000;
            return Vector2.Distance(shipTransform.position, targetTransform.position);
        }

        public void SetSceneActorTracker(IActorTracker actorTracker) { sceneActorTracker = actorTracker; }
    }
}

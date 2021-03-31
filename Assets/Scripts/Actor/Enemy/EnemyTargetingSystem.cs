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
    }

    public class EnemyTargetingSystem : MonoBehaviour, IAssignSceneActorTracker, IEnemyTargetingSystem
    {
        // Fields
        private Transform targetTransform = null;
        private IActorTracker sceneActorTracker;

        public void InitialiseTargetingSystem()
        {
        }

        // Selects the nearest target relative to this posiiton. The system will already 'know' where
        // the players are in the game, and decide on action based on the relative proximity to the player,
        // and the climate of battle in the scene.
        public void SelectNearestTarget()
        {
            targetTransform = sceneActorTracker.GetRandomFriendly().transform;
        }

        public Transform GetTargetTransform() { return targetTransform; }

        public void SetSceneActorTracker(IActorTracker actorTracker) { sceneActorTracker = actorTracker; }
    }
}

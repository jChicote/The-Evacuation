using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Actor.EnemySystems
{
    public interface IStateManager
    {
        void InitialiseStateManager();
        void AddState<T>();
        void RemoveState(BaseComponentState state);
    }

    public class EnemyStateManager : ActorStateManager
    {
        [SerializeField] private Timer stateTimer;

        private BaseComponentState currentState;

        public override void AddState<T>()
        {
            if (currentState != null)
                this.RemoveState();

            currentState = this.gameObject.AddComponent<T>();
            currentState.BeginState();
        }

        public override void RemoveState()
        {
            if (currentState != null)
            {
                currentState.EndState();
                Destroy(currentState);
            }
        }
    }
}

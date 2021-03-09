using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Actor
{
    public interface IStateManager 
    {
        void IntialiseStateManager();
        void AddState<T>();
        void RemoveState();
    }

    public abstract class ActorStateManager : MonoBehaviour
    {
        public virtual void IntialiseStateManager() { }

        public abstract void AddState<T>() where T : BaseComponentState;

        public abstract void RemoveState();
    }
}

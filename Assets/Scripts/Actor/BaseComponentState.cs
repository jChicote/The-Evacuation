using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Actor
{
    public interface IStateRuntime
    {
        void RunStateUpdate();
    }

    public abstract class BaseComponentState : MonoBehaviour, IStateRuntime, IPausable
    {
        // Fields
        protected bool isPaused = false;

        public abstract void BeginState();

        public virtual void EndState() { }

        public void OnPause()
        {
            print("Is Called");
            isPaused = true;
        }

        public void OnUnpause()
        {
            isPaused = false;
        }

        public virtual void RunStateUpdate() { }
    }
}

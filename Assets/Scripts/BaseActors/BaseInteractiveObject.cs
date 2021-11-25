using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheEvacuation
{
    public interface IPausable
    {
        void OnPause();
        void OnUnpause();
    }

    public interface ICheckPaused
    {
        bool CheckIsPaused();
    }

    public class BaseInteractiveObject : MonoBehaviour, IPausable
    {
        [SerializeField]
        private bool isPaused = false;

        public virtual void OnPause()
        {
            isPaused = true;
        }

        public virtual void OnUnpause()
        {
            isPaused = false;
        }
    }

}
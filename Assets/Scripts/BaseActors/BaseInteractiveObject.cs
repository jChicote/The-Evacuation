using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheEvacuation
{
    public interface IPausable
    {
        bool IsPaused { get; set; }
        void OnPause();
        void OnUnpause();
    }

    public interface ICheckPaused
    {
        bool CheckIsPaused();
    }

    public class BaseInteractiveObject : MonoBehaviour, IPausable
    {
        // Fields
        [SerializeField]
        public bool isPaused = false;

        // Properties
        public bool IsPaused { get => isPaused; set => isPaused = value; }

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
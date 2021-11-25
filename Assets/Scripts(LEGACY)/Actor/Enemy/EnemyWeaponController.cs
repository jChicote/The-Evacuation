using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Actor.EnemySystems
{
    public interface IWeaponController
    {
        void InitialiseWeaponController();
        void RunWeaponSystem();
    }

    public class EnemyWeaponController : MonoBehaviour, IWeaponController, IPausable
    {
        // Fields
        protected bool isPaused = false;
        protected IWeapon[] weapons;

        public virtual void InitialiseWeaponController() { }

        public void OnPause()
        {
            isPaused = true;
        }

        public void OnUnpause()
        {
            isPaused = false;
        }

        public virtual void RunWeaponSystem() { }
    }
}

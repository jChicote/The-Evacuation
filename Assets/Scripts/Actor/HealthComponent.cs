using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Actor
{
    public interface IHealthComponent
    {
        void InitialiseHealth(float maxHealth);
        void SetHealthUpdate(float healthValue);
        float GetShipHealth();
        bool IsActive();
    }

    public class HealthComponent : MonoBehaviour, IHealthComponent
    {

        protected bool isHealthRegenerating = false;

        public virtual void InitialiseHealth(float maxHealth) { }

        public virtual void SetHealthUpdate(float healthValue) { }

        public virtual float GetShipHealth() { return 0; }

        public virtual bool IsActive() 
        {
            print(
                "This is the default base implementation of this method. " +
                "Please use a child version of this module."
                );
            return false; 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Actor;

namespace Evacuation.PlayerSystems
{
    public class PlayerDamageManager : ActorDamageManager
    {
        //private IHealthAccessors healthAccessors;
        private PlayerHeathComponent healthComponent;

        public override void InitialiseComponent()
        {
           // healthAccessors = this.GetComponent<IHealthAccessors>();
            healthComponent = this.GetComponent<PlayerHeathComponent>();
        }

        public override void OnDamage(float damage)
        {
            Debug.Log("Damage at: " + damage);
            if (healthComponent.IsActive())
            {
                float newHealth = healthComponent.CalculateDamagedHealth(damage);
                healthComponent.SetHealthUpdate(newHealth);
            }
        }
    }
}

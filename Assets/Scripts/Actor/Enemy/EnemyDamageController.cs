using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Actor.EnemySystems
{
    public class EnemyDamageController : ActorDamageController
    {
        private IHealthComponent healthComponent;

        public override void InitialiseComponent()
        {
            healthComponent = this.GetComponent<IHealthComponent>();
        }

        public override void OnDamage(float damage)
        {
            if (healthComponent.IsActive())
            {
                float newHealth = healthComponent.GetShipHealth() - damage;
                healthComponent.SetHealthUpdate(newHealth);
            }
        }
    }
}

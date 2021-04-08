using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Actor.EnemySystems
{
    public class EnemyDamageManager : ActorDamageManager
    {
        private EnemyHealthComponent healthComponent;

        public override void InitialiseComponent()
        {
            healthComponent = this.GetComponent<EnemyHealthComponent>();
        }

        public override void OnDamage(float damage)
        {
            if (healthComponent.IsActive())
            {
                float newHealth = healthComponent.CalculateDamagedHealth(damage);
                healthComponent.SetHealthUpdate(newHealth);
            }
        }
    }
}

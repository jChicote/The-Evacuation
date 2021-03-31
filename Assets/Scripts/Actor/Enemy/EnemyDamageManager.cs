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
            // healthAccessors = this.GetComponent<IHealthAccessors>();
            healthComponent = this.GetComponent<EnemyHealthComponent>();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Actor;

namespace Evacuation.Actor.EnemySystems
{

    public class EnemyHealthComponent : HealthComponent
    {
        private IHealthAccessors healthAccessors;
        protected IStateManager stateManager;

        public override void InitialiseHealth(float maxHealth)
        {
            healthAccessors = this.GetComponent<IHealthAccessors>();
            stateManager = this.GetComponent<IStateManager>();
        }

        public override void SetHealthUpdate(float healthValue)
        {
            if (healthValue <= 90) //test
            {
                stateManager.AddState<EnemyDeathState>();
            }

            healthAccessors.SetShipHealth(healthValue);
        }

        public override float GetShipHealth()
        {
            return healthAccessors.GetShipHealth();
        }

        public override bool IsActive()
        {
            return healthAccessors.GetShipHealth() > 0;
        }
    }

}
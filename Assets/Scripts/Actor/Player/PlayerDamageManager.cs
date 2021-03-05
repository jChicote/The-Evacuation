using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void InitialiseComponent();
    void OnDamage(float damage);
}

namespace Evacuation.PlayerSystems
{
    public class PlayerDamageManager : MonoBehaviour, IDamageable
    {
        //private IHealthAccessors healthAccessors;
        private PlayerHeathComponent healthComponent;

        public void InitialiseComponent()
        {
           // healthAccessors = this.GetComponent<IHealthAccessors>();
            healthComponent = this.GetComponent<PlayerHeathComponent>();
        }

        public void OnDamage(float damage)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UserInterface.HUD;

namespace PlayerSystems
{
    public class PlayerHeathComponent : HealthComponent
    {
        private IVitalityBar healthBar;
        private IHealthAccessors healthAccessors;

        public override void InitialiseHealth(float maxHealth)
        {
            healthAccessors = this.GetComponent<IHealthAccessors>();

            GameObject playerHud = GameObject.FindGameObjectWithTag("HUD");
            IHudAccessors vitalityAccessors = playerHud.GetComponent<IHudAccessors>();
            healthBar = vitalityAccessors.GetHealthBar();
            healthBar.InitialiseBar(maxHealth);
        }

        public float CalculateDamagedHealth(float damage)
        {
            return healthAccessors.GetShipHealth() - damage;
        }

        public override void SetHealthUpdate(float healthValue)
        {
            if (isHealthRegenerating)
            {

            }

            healthAccessors.SetShipHealth(healthValue);
            healthBar.SetBarValue(healthValue);
        }

        public bool IsActive()
        {
            return healthAccessors.GetShipHealth() > 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UserInterfaces.HUD;

namespace PlayerSystems
{
    public class PlayerShieldComponent : ShieldComponent
    {
        private IVitalityBar shieldBar;
        private IShieldAccessors shieldAccessors;


        public override void InitialiseShield(float maxShield)
        {
            shieldAccessors = this.GetComponent<IShieldAccessors>();

            GameObject playerHud = GameObject.FindGameObjectWithTag("HUD");
            IHudAccessors vitalityAccessors = playerHud.GetComponent<IHudAccessors>();
            shieldBar = vitalityAccessors.GetShieldBar();
            shieldBar.InitialiseBar(maxShield);
        }

        public float CalculateShieldDamage(float damage)
        {
            return shieldAccessors.GetShipShields() - damage;
        }

        public override void SetShieldUpdate(float healthValue)
        {
            if (isShieldRegenerating)
            {

            }

            shieldAccessors.SetShipShields(healthValue);
            shieldBar.SetBarValue(healthValue);
        }

        public bool IsActive()
        {
            return shieldAccessors.GetShipShields() > 0;
        }
    }
}

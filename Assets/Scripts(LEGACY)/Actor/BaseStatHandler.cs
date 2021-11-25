using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Actor
{
    public class BaseStatHandler : MonoBehaviour, IHealthAccessors, IModifier
    {
        [Header("Primary Hit Points")]
        public float currentHealth = 100;
        public float currentShield = 0;

        [Header("Ship Sub Stats")]
        public Modifier shipModifier;

        [Header("Event Observers")]
        public ShipHitPoint OnHealthChanged;
        public ShipHitPoint OnShieldChanged;

        public Modifier GetShipModifiers()
        {
            return shipModifier;
        }

        public float GetShipHealth()
        {
            return currentHealth;
        }

        public void SetShipHealth(float newHealth)
        {
            currentHealth = newHealth;
        }

        public float GetShipShields()
        {
            return currentShield;
        }

        public void SetShipShields(float newShields)
        {
            currentShield = newShields;
        }
    }

}
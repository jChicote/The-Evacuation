using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Model.Data
{
    public struct ShipData
    {
        public ShipData(
            int rescueCap,
            float maxHealth,
            float maxShield,
            float maxSpeed,
            float maxHandling,
            List<string> fWeapon,
            List<string> tWeapon)
        {
            this.rescueCapacity = rescueCap;
            Debug.Log(fWeapon.Count);

            this.maxHealth = maxHealth;
            this.maxShield = maxShield;
            this.maxSpeed = maxSpeed;
            this.maxHandling = maxHandling;
            this.fixedWeapons = fWeapon;
            this.turrentWeapons = tWeapon;
        }

        // Core Stats
        private int rescueCapacity;
        public int RescueCapacity => rescueCapacity;

        private float maxHealth;
        public float MaxHealth => maxHealth;

        private float maxShield;
        public float MaxShield => maxShield;

        private float maxSpeed;
        public float MaxSpeed => maxSpeed;

        private float maxHandling;
        public float MaxHandling => maxHandling;


        // Weapoon Loadouts
        private List<string> fixedWeapons;
        public List<string> FixedWeapons => fixedWeapons;

        private List<string> turrentWeapons;
        public List<string> TurrentWeapons => turrentWeapons;
    }
}

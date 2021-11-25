using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Evacuation.Model.Data;

namespace Evacuation.Actor
{
    public interface IStatHandler : IStatRescue
    {
        void InitialiseStats(ShipData shipData);
        ShipData GetShipData();
    }

    public interface IStatRescue
    {
        int RescueCabinCount { get; set; }
        //int CabinCapacity { get; }
    }

    /*public interface IPlayerWeaponStats
    {
        List<string> GetForwardLoadout();
        List<string> GetTurrentLoadout();
    }*/

    public interface IModifier
    {
        Modifier GetShipModifiers();
    }

    public interface IPlayerStats
    {
        ShipData GetShipData();
    }

    public interface IHealthAccessors
    {
        float GetShipHealth();
        void SetShipHealth(float newHealth);
    }

    public interface IShieldAccessors
    {
        float GetShipShields();
        void SetShipShields(float newShields);
    }

    public class StatHandler : BaseStatHandler, IStatHandler, IPlayerStats
    {
        [Header("Ship Sub Stats")]
        [SerializeField] private ShipData shipData;

        // Fields
        protected int rescueCabinCount = 0;

        public void InitialiseStats(ShipData shipData)
        {
            this.shipData = shipData;

            SubscribeToEvents();
        }

        /// <summary>
        /// Subscribes class events to external entities or UI.
        /// </summary>
        protected void SubscribeToEvents()
        {
            OnHealthChanged = new ShipHitPoint();
            OnShieldChanged = new ShipHitPoint();

            //TODO: Subscribe UI items
        }

        // Accessors
        public int RescueCabinCount
        {
            get { return rescueCabinCount; }
            set { rescueCabinCount = value; }
        }

        /*
        public int CabinCapacity
        {
            get { return shipInfo.rescueCapacity; }
        }

        public List<string> GetForwardLoadout()
        {
            return shipInfo.fixedWeapons;
        }

        public List<string> GetTurrentLoadout()
        {
            return shipInfo.turrentWeapons;
        }*/

        public ShipData GetShipData()
        {
            return shipData;
        }
    }
}

[System.Serializable]
public class ShipHitPoint : UnityEvent<float> { }

[System.Serializable]
public struct Modifier
{
    public float speedModifier;
    public float shieldModifier;
    public float healthModifier;
    public float fireRateModifier;
    public float damageModifier;
}

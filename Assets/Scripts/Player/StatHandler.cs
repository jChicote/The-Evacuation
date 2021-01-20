using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IStatHandler {
    void InitialiseStats();
}

public interface IWeaponStats {
    Loadout[] GetForwardLoadout();
    Loadout[] GetTurrentLoadout();
}

public interface IShipData
{
    Modifier GetShipModifiers();
    ShipStats GetShipStats();
}

public interface IShipHealth
{
    float GetShipHealth();
    void SetShipHealth(float newHealth);
}

public interface IShipShields 
{
    float GetShipShields();
    void SetShipShields(float newShields);
}

public class StatHandler : MonoBehaviour, IStatHandler, IWeaponStats, IShipData, IShipHealth, IShipShields
{
    [Header("Primary Hit Points")]
    public float currentHealth = 100;
    public float currentShield = 0;

    [Header("Ship Sub Stats")]
    public ShipStats shipStats;
    public Modifier shipModifier;

    [Header("Event Observers")]
    public ShipHitPoint OnHealthChanged;
    public ShipHitPoint OnShieldChanged;

    public void InitialiseStats()
    {
        // Gets ships stats from the p0ublic inventory
        // Currently uses the defaults from the settings
        Shipinfo retrievedInfo = GameManager.Instance.playerSettigns.shipsList[0];
        shipStats = retrievedInfo.stats;

        SubscribeToEvents();
    }

    /// <summary>
    /// Subscribes class events to external entities or UI.
    /// </summary>
    private void SubscribeToEvents()
    {
        OnHealthChanged = new ShipHitPoint();
        OnShieldChanged = new ShipHitPoint();

        //TODO: Subscribe UI items
    }

    public Loadout[] GetForwardLoadout()
    {
        return shipStats.forwardLoadouts;
    }

    public Loadout[] GetTurrentLoadout()
    {
        return shipStats.turrentLoadouts;
    }

    public Modifier GetShipModifiers()
    {
        return shipModifier;
    }

    public ShipStats GetShipStats()
    {
        return shipStats;
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

[System.Serializable]
public class ShipStats
{
    [Tooltip("A copy is necessary as the struct is what is passed into ship prefabs")]
    public string shipID;

    public float maxHealth = 100;
    public float maxSheild = 100;

    [Header("Movement")]
    public float maxSpeed = 20;
    public float maxHandling = 0;

    [Header("Weapon Loadouts")]
    public Loadout[] forwardLoadouts;
    public Loadout[] turrentLoadouts;
}

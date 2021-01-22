using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class containng the fields necessary for the ship and UTILISED for data storage.
/// </summary>
[System.Serializable]
public class ShipInfo : ObjectInfo
{
    // This class is a more lightweight configuration made for storage on device locally and does not draw from settings.

    // Core Stats
    public float maxHealth;
    public float maxSheild;
    public float maxSpeed;
    public float maxHandling;

    // Weapoon Loadouts
    public List<WeaponInfo> forwardWeapons;
    public List<WeaponInfo> turrentWeapons;

    public void SetData(string stringID, string name, float price, float maxHealth, float maxShield, float maxSpeed, float maxHandling, List<WeaponInfo> forwardWeapons, List<WeaponInfo> turrentWeapons)
    {
        this.stringID = stringID;
        this.name = name;
        this.price = price;

        this.maxHealth = maxHealth;
        this.maxHealth = maxShield;
        this.maxSpeed = maxSpeed;
        this.maxHandling = maxHandling;

        this.forwardWeapons = forwardWeapons;
        this.turrentWeapons = turrentWeapons;
    }
}
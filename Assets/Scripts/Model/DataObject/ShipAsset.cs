using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Actor;

/// <summary>
/// A class with fields generalising characteristics of the object but NOT used for stoage.
/// </summary>
[System.Serializable]
public class ShipAsset : ObjectInfo
{
    // Ship asset is not stored directly into session but is referred from the scriptable object
    [TextArea(5, 15)]
    public string description;
    public int requiredLevel;

    [Header("Data")]
    public ShipStats stats;
    public Sprite image;

    [Header("Prefab")]
    public GameObject shipPrefab;

    public ShipInfo ConvertToShipInfo()
    {
        ShipInfo info = new ShipInfo();

        info.SetData(
            stringID,
            name,
            price,
            stats.maxRescueCapacity,
            stats.maxHealth,
            stats.maxSheild,
            stats.maxSpeed,
            stats.maxHandling,
            stats.forwardSize,
            stats.turrentSize);

        return info;
    }
}

[System.Serializable]
public class ShipStats : BaseStats
{
    public int maxRescueCapacity = 5;

    [Header("Weapon Loadouts")]
    public int forwardSize;
    public int turrentSize;
}

public class BaseStats
{
    [Tooltip("A copy is necessary as the struct is what is passed into ship prefabs")]
    public string shipID;
    public float maxHealth = 100;
    public float maxSheild = 100;

    [Header("Movement")]
    public float maxSpeed = 20;
    public float maxHandling = 0;
}

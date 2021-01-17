using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatHandler {

}

public class StatHandler : MonoBehaviour
{

    public float currentHealth = 100;
    public float currentShield = 0;

    public Modifier statModifier;
}

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

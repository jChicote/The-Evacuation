using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponInfo : ObjectInfo
{
    // Note that the global ID is universally referrs to the weapon 
    // and not the instance of the weapon
    public string globalID;

    // Fields
    public WeaponType weaponType;
    public float damage;
    public float speed;
    public float fireRate;
    public float life;
    public int spread;
    public bool isAttached = false;

    public void SetData(WeaponType weaponType, string universalID, string name, int price, float damage, float speed, float fireRate, float life, int spread)
    {
        this.weaponType = weaponType;

        //Code ID combination is: # + ObjectName + count
        this.instanceID = CreateUniqueID(name);
        this.globalID = universalID;
        this.name = name;
        this.price = price;
        this.damage = damage;
        this.speed = speed;
        this.fireRate = fireRate;
        this.life = life;
        this.spread = spread;
    }

    private string CreateUniqueID(string name)
    {
        return name + System.DateTime.Today.ToString() + Random.Range(0, 500);
    }
}

public enum WeaponType
{
    Turrent,
    Laser,
    Launcher
}

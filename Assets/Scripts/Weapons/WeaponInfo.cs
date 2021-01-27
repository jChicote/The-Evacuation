using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponInfo : ObjectInfo
{
    public WeaponType weaponType;
    public float damage;
    public float speed;
    public float fireRate;
    public float life;
    public int spread;
    public bool isAttached = false;

    public void SetData(WeaponType weaponType, string stringID, string name, float price, float damage, float speed, float fireRate, float life, int spread)
    {
        this.weaponType = weaponType;

        //Code ID combination is: # + ObjectName + count
        this.stringID = CreateUniqueID(name);
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

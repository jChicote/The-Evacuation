using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        this.stringID = stringID;
        this.name = name;
        this.price = price;
        this.damage = damage;
        this.speed = speed;
        this.fireRate = fireRate;
        this.life = life;
        this.spread = spread;
    }
}

public enum WeaponType
{
    Turrent,
    Laser,
    Launcher
}

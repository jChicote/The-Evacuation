using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponAsset : ObjectInfo
{
    [Header("Data")]
    public string globalID;
    public WeaponData defaultData;

    [TextArea(5, 15)]
    public string description;

    [Header("Prefabs")]
    public GameObject weaponPrefab;

    [Header("Thumbanil")]
    public Sprite spriteThumbnail;

    public WeaponInfo ConvertToWeaponInfo()
    {
        WeaponInfo info = new WeaponInfo();

        info.SetData(
            defaultData.weaponType,
            globalID,
            name,
            price,
            defaultData.damage,
            defaultData.speed,
            defaultData.fireRate,
            defaultData.life,
            defaultData.spread);

        return info;
    }
}

[System.Serializable]
public struct WeaponData
{
    //public string stringID;
    public WeaponType weaponType;

    [Header("Data Members")]
    public float damage;
    public float speed;
    public float fireRate;
    public float life;
    public int spread;
}

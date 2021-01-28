using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Settings/Weapon Setting")]
public class WeaponSettings : ScriptableObject
{
    [Header("Weapons")]
    public List<WeaponAsset> turrentWeapons;
    public List<WeaponAsset> laserWeapon;
    public List<WeaponAsset> launcherWeapons;

    /// <summary>
    /// Retrieves the weapon from different assortment of lists.
    /// </summary>
    public WeaponAsset RetrieveFromSettings(WeaponType type, string name)
    {
        switch (type)
        {
            case WeaponType.Turrent:
                return SearchThroughList(GameManager.Instance.weaponSettings.turrentWeapons, name);
            case WeaponType.Laser:
                return SearchThroughList(GameManager.Instance.weaponSettings.laserWeapon, name);
            case WeaponType.Launcher:
                return SearchThroughList(GameManager.Instance.weaponSettings.launcherWeapons, name);
        }

        return null;
    }

    /// <summary>
    /// Retrieves asset from specified asset list.
    /// </summary>
    private WeaponAsset SearchThroughList(List<WeaponAsset> assetList, string name)
    {
        foreach (WeaponAsset asset in assetList)
        {
            if (asset.name == name)
            {
                return asset;
            }
        }

        return null;
    }
}
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
    public WeaponAsset RetrieveFromSettings(WeaponType type, string globalID)
    {
        WeaponSettings weaponSettings = GameManager.Instance.weaponSettings;

        switch (type)
        {
            case WeaponType.Turrent:
                return SearchThroughList(weaponSettings.turrentWeapons, globalID);
            case WeaponType.Laser:
                return SearchThroughList(weaponSettings.laserWeapon, globalID);
            case WeaponType.Launcher:
                return SearchThroughList(weaponSettings.launcherWeapons, globalID);
        }

        return null;
    }

    /// <summary>
    /// Retrieves asset from specified asset list.
    /// </summary>
    private WeaponAsset SearchThroughList(List<WeaponAsset> assetList, string globalID)
    {
        foreach (WeaponAsset asset in assetList)
        {
            if (asset.globalID == globalID)
            {
                return asset;
            }
        }

        return null;
    }
}
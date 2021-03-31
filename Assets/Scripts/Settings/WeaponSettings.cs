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
    public WeaponAsset RetrieveFromSettings(WeaponType type, string universalID)
    {
        switch (type)
        {
            case WeaponType.Turrent:
                return SearchThroughList(GameManager.Instance.weaponSettings.turrentWeapons, universalID);
            case WeaponType.Laser:
                return SearchThroughList(GameManager.Instance.weaponSettings.laserWeapon, universalID);
            case WeaponType.Launcher:
                return SearchThroughList(GameManager.Instance.weaponSettings.launcherWeapons, universalID);
        }

        return null;
    }

    /// <summary>
    /// Retrieves asset from specified asset list.
    /// </summary>
    private WeaponAsset SearchThroughList(List<WeaponAsset> assetList, string universalID)
    {
        foreach (WeaponAsset asset in assetList)
        {
            if (asset.universalID == universalID)
            {
                return asset;
            }
        }

        return null;
    }
}
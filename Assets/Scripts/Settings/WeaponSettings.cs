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
    public WeaponAsset RetrieveFromSettings(WeaponType type, string stringID)
    {
        switch (type)
        {
            case WeaponType.Turrent:
                return SearchThroughList(GameManager.Instance.weaponSettings.turrentWeapons, stringID);
            case WeaponType.Laser:
                return SearchThroughList(GameManager.Instance.weaponSettings.laserWeapon, stringID);
            case WeaponType.Launcher:
                return SearchThroughList(GameManager.Instance.weaponSettings.launcherWeapons, stringID);
        }

        return null;
    }

    /// <summary>
    /// Retrieves asset from specified asset list.
    /// </summary>
    private WeaponAsset SearchThroughList(List<WeaponAsset> assetList, string stringID)
    {
        foreach (WeaponAsset asset in assetList)
        {
            if (asset.stringID == stringID)
            {
                return asset;
            }
        }

        return null;
    }
}
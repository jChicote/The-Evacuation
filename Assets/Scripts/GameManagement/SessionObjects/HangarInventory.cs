using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HangarInventory
{
    public List<WeaponInfo> hangarWeapons = new List<WeaponInfo>();
    public List<ShipInfo> hangarShips = new List<ShipInfo>();

    public void UpdateHangarWeapons(List<WeaponInfo> update)
    {
        hangarWeapons = update;
        Debug.Log("Updated hangar weapons");
    }

    public List<WeaponInfo> GetHangarWeapons()
    {
        return hangarWeapons;
    }

    public void UpdateHangarShips(List<ShipInfo> update)
    {
        hangarShips = update;
        Debug.Log("Updated hangar ships");
    }

    public List<ShipInfo> GetHangarShips()
    {
        return hangarShips;
    }
}


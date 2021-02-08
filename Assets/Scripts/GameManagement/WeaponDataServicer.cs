using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponDataServicer
{
    // Responisble for handling logic associated with weapon organisation in data and 
    // retruieval of data items from arrays.
    private SessionData instance;

    public WeaponDataServicer()
    {
        this.instance = SessionData.instance;
    }

    /// <summary>
    /// Gets the weapon info of the specified weapon
    /// </summary>
    public WeaponInfo GetWeaponItem(string weaponID)
    {
        // To limit the clunckiness of passing weapon info around, each vessel will instead store only the string reference.

        return instance.hangarCurrentSave.hangarWeapons.Where(x => x.stringID == weaponID).First();
    }

    /// <summary>
    /// Gets the available weapon count in the hangar
    /// </summary>
    public int GetAvailableWeaponInstanceCount(string universalID)
    {
        int count = instance.hangarCurrentSave.hangarWeapons.Where(x => x.universalID == universalID && x.isAttached == false).Count();
        return count;
    }

    /// <summary>
    /// Removes weapon instance from list
    /// </summary>
    public void RemoveWeaponInstance(string universalID)
    {
        WeaponInfo removedObject = instance.hangarCurrentSave.hangarWeapons.Where(x => x.universalID == universalID && x.isAttached == false).First();
        instance.hangarCurrentSave.hangarWeapons.Remove(removedObject);
    }

    /// <summary>
    /// Adds weapon instance into hangar
    /// </summary>
    public void AddWeaponInstance(WeaponAsset asset)
    {
        WeaponInfo info = asset.ConvertToWeaponInfo();
        instance.hangarCurrentSave.hangarWeapons.Add(info);
    }
}

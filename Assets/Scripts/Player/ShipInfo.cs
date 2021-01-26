using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public interface IAssignWeapon
{
    /// <summary>
    /// Called to assign weapons to the specified loadout position.
    /// </summary>
    /// <param name="weaponConfig"></param>
    /// <param name="weaponID"></param>
    /// <param name="indexPosition"></param>
    void AssignWeapons(WeaponConfiguration weaponConfig, string weaponID, int indexPosition);

    /// <summary>
    /// Called to remove the weapons from the specified loadout and index.
    /// </summary>
    /// <param name="weaponConfig"></param>
    /// <param name="indexPosition"></param>
    void RemoveWeapon(WeaponConfiguration weaponConfig, int indexPosition);
}

/// <summary>
/// A class containng the fields necessary for the ship and UTILISED for data storage.
/// </summary>
[System.Serializable]
public class ShipInfo : ObjectInfo, IAssignWeapon
{
    // This class is a more lightweight configuration made for storage on device locally and does not draw from settings.

    // Core Stats
    public float maxHealth;
    public float maxSheild;
    public float maxSpeed;
    public float maxHandling;

    // Weapoon Loadouts
    public List<string> forwardWeapons;
    public List<string> turrentWeapons;

    public void SetData(string stringID, string name, float price, float maxHealth, float maxShield, float maxSpeed, float maxHandling, int forwardWeaponSize, int turrentWeaponSize)
    {
        this.stringID = stringID;
        this.name = name;
        this.price = price;

        this.maxHealth = maxHealth;
        this.maxHealth = maxShield;
        this.maxSpeed = maxSpeed;
        this.maxHandling = maxHandling;

        this.forwardWeapons = new List<string>(new string[forwardWeaponSize]);
        PopulateListToDefault(forwardWeapons);
        this.turrentWeapons = new List<string>(new string[turrentWeaponSize]);
        PopulateListToDefault(turrentWeapons);
    }

    private void PopulateListToDefault(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = "";
        }
    }

    public void AssignWeapons(WeaponConfiguration weaponConfig, string weaponID, int indexPosition)
    {
        if (weaponConfig == WeaponConfiguration.Forward)
        {
            RemoveWeapon(weaponConfig, indexPosition);
            AllocateWeaponToList(weaponID, indexPosition, forwardWeapons);
        } 
        else if(weaponConfig == WeaponConfiguration.Turrent)
        {
            RemoveWeapon(weaponConfig, indexPosition);
            AllocateWeaponToList(weaponID, indexPosition, turrentWeapons);
        }
    }

    /// <summary>
    /// Allocates weapons to their their position if existent.
    /// </summary>
    private void AllocateWeaponToList(string weaponID, int indexPosition, List<string> weaponList)
    {
        HangarInventory inventory = SessionData.instance.hangarCurrentSave;
        string extractedWeaponID = "";

        for (int i = 0; i < inventory.hangarWeapons.Count; i++)
        {
            if(extractedWeaponID == "")
            {
                if (inventory.hangarWeapons[i].stringID.Equals(weaponID) && !inventory.hangarWeapons[i].isAttached)
                {
                    extractedWeaponID = inventory.hangarWeapons[i].stringID;
                    inventory.hangarWeapons[i].isAttached = true;
                }
            }
        }

        // Check if array search turned up with a result.
        if (extractedWeaponID != "")
        {
            //extractedWeaponID.isAttached = true;
            weaponList[indexPosition] = extractedWeaponID;
        }
    }

    //TODO: Consider changing the weapons to rely on a string reference to the unique weapon in the inventory (UTTILISE HASHING)
    public void RemoveWeapon(WeaponConfiguration weaponConfig, int indexPosition)
    {
        if (weaponConfig == WeaponConfiguration.Forward)
        {
            if (forwardWeapons[indexPosition] == null) return;

            HangarInventory inventory = SessionData.instance.hangarCurrentSave;
            string extractedWeaponID = null;

            for (int i = 0; i < inventory.hangarWeapons.Count; i++)
            {
                if (extractedWeaponID == "")
                {
                    if (inventory.hangarWeapons[i].stringID.Equals(forwardWeapons[indexPosition]) && !inventory.hangarWeapons[i].isAttached)
                    {
                        extractedWeaponID = inventory.hangarWeapons[i].stringID;
                        inventory.hangarWeapons[i].isAttached = false;
                    }
                }
            }

            forwardWeapons[indexPosition] = "";
        }
    }
}

public enum WeaponConfiguration
{
    Forward,
    Turrent
}
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public interface IAssignWeapon
{
    /// <summary>
    /// Called to assign weapons to the specified loadout position.
    /// </summary>
    void AssignWeapons(WeaponConfiguration weaponConfig, string weaponID);

    /// <summary>
    /// Called to remove the weapons from the specified loadout and index.
    /// </summary>
    void RemoveWeapon(WeaponConfiguration weaponConfig, string stringID);
}

/// <summary>
/// A class containng the fields necessary for the ship and UTILISED for data storage.
/// </summary>
[System.Serializable]
public class ShipInfo : ObjectInfo, IAssignWeapon
{
    // This class is a more lightweight configuration made for storage on device locally and does not draw from settings.
    public bool isUnlocked = false;

    // Core Stats
    public float maxHealth;
    public float maxSheild;
    public float maxSpeed;
    public float maxHandling;

    // Weapoon Loadouts
    public List<string> forwardWeapons;
    public List<string> turrentWeapons;

    public void SetData(string stringID, string name, int price, float maxHealth, float maxShield, float maxSpeed, float maxHandling, int forwardWeaponSize, int turrentWeaponSize)
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

    /// <summary>
    /// Called to populate a weapon list to default empty values.
    /// </summary>
    /// <param name="list"></param>
    private void PopulateListToDefault(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = "";
        }
    }

    /// <summary>
    /// Called to assign weapons to the specified weapon configuration.
    /// </summary>
    public void AssignWeapons(WeaponConfiguration weaponConfig, string weaponID)
    {
        if (weaponConfig == WeaponConfiguration.Forward)
        {
            AllocateWeaponToList(weaponID, forwardWeapons);
        }
        else if (weaponConfig == WeaponConfiguration.Turrent)
        {
            AllocateWeaponToList(weaponID, turrentWeapons);
        }
    }

    /// <summary>
    /// Allocates weapons to their their position if existent.
    /// </summary>
    private void AllocateWeaponToList(string weaponID, List<string> weaponList)
    {
        List<WeaponInfo> hangarWeapons = SessionData.instance.weaponServicer.GetHangarWeapons();
        int indexPosition = GetFirstEmptySlot(weaponList);
        string extractedWeaponID = "";

        for (int i = 0; i < hangarWeapons.Count; i++)
        {
            if(extractedWeaponID == "")
            {
                if (hangarWeapons[i].stringID.Equals(weaponID) && !hangarWeapons[i].isAttached)
                {
                    extractedWeaponID = hangarWeapons[i].stringID;
                    hangarWeapons[i].isAttached = true;
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

    /// <summary>
    /// 
    /// </summary>
    public void RemoveWeapon(WeaponConfiguration weaponConfig, string stringID)
    {
        List<WeaponInfo> hangarWeapons = SessionData.instance.weaponServicer.GetHangarWeapons();

        if (weaponConfig == WeaponConfiguration.Forward)
        {
            int indexPosition = GetEquipmentPosition(forwardWeapons, stringID);
            forwardWeapons[indexPosition] = "";

            // Changes info in weapon hangar to be unattached
            hangarWeapons.Where(x => x.stringID == stringID).First().isAttached = false;
        } 
        else
        {
            int indexPosition = GetEquipmentPosition(turrentWeapons, stringID);
            turrentWeapons[indexPosition] = "";

            // Changes info in weapon hangar to be unattached
            hangarWeapons.Where(x => x.stringID == stringID).First().isAttached = false;
        }
    }

    /// <summary>
    /// Determines the index of the first available slot.
    /// </summary>
    private int GetFirstEmptySlot(List<string> weaponList)
    {
        int availableIndex = 100000;
        bool isAvailable = false;

        for (int i = 0; i < weaponList.Count; i++)
        {
            if (weaponList[i] == "" && !isAvailable)
            {
                availableIndex = i;
                isAvailable = true;
            }
        }

        return availableIndex;
    }

    /// <summary>
    /// Gets the equipment position in the list.
    /// </summary>
    private int GetEquipmentPosition(List<string> list, string equipmentID)
    {
        int locatedIndex = 100000;
        bool isLocated = false;

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == equipmentID && !isLocated)
            {
                locatedIndex = i;
                isLocated = true;
            }
        }

        return locatedIndex;
    }

    /// <summary>
    /// Gets the weapon ships from the ship based on the specified configuration.
    /// </summary>
    public List<string> GetWeaponsList(WeaponConfiguration configuration)
    {
        if (configuration == WeaponConfiguration.Forward)
        {
            return forwardWeapons;
        }
        else
        {
            return turrentWeapons;
        }
    }

    /// <summary>
    /// Checks if all weapon arrays are full.
    /// </summary>
    public bool CheckIsFull(WeaponConfiguration configuration)
    {
        if (configuration == WeaponConfiguration.Forward)
        {
            return forwardWeapons.Where(x => x == "").Count() == 0;
        }
        else
        {
            return turrentWeapons.Where(x => x == "").Count() == 0;
        }
    }

    public void UnlockShip(bool state)
    {
        isUnlocked = state;
    }
}

public enum WeaponConfiguration
{
    Forward,
    Turrent
}
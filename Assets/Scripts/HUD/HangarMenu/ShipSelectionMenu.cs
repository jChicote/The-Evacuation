using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public interface IIdentifyShip
{
    string GetShipID();
}

public interface IShipAssign
{
    /// <summary>
    /// Assigns to ship but returns false if no space is available.
    /// </summary>
    /// <param name="stringID"></param>
    /// <returns></returns>
    bool AssignItem(string stringID);
}

public interface ICheckShipSlot
{
    bool CheckAvailableSlot();
}


public class ShipSelectionMenu : MonoBehaviour, IIdentifyShip, IShipAssign, ICheckShipSlot
{
    public WeaponConfiguration weaponConfig;

    [Header("Content Attributes")]
    public Image shipImage;
    public GameObject contentView;

    private string shipID;

    public bool AssignItem(string stringID)
    {
        return false;
    }

    public bool CheckAvailableSlot()
    {
        if (weaponConfig == WeaponConfiguration.Forward)
        {
            ShipInfo info = SessionData.instance.hangarCurrentSave.hangarShips.Where(x => x.stringID == this.shipID).First();
            return info.forwardWeapons.Where(x => x == null).First() == null;
        } else
        {
            ShipInfo info = SessionData.instance.hangarCurrentSave.hangarShips.Where(x => x.stringID == this.shipID).First();
            return info.turrentWeapons.Where(x => x == null).First() == null;
        }
    }

    public void DisplayForwardInventory()
    {
        weaponConfig = WeaponConfiguration.Forward;
    }

    public void DisplayTurrentInventory()
    {
        weaponConfig = WeaponConfiguration.Turrent;
    }

    public string GetShipID()
    {
        return shipID;
    }
}

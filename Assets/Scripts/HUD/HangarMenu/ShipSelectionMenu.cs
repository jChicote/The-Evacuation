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
    bool CheckSlotAvailability();
}


public class ShipSelectionMenu : MonoBehaviour, IIdentifyShip, IShipAssign, ICheckShipSlot
{
    public EquipmentType equipmentType;

    [Header("Content Attributes")]
    public Image shipImage;
    public GameObject contentView;

    private string shipID;

    public void PopulateInventoryList()
    {

    }

    public bool AssignItem(string equipmentID)
    {
        // Get the selected ship
        List<ShipInfo> hangarShips = SessionData.instance.hangarCurrentSave.GetHangarShips();

        ShipInfo selectedShip = hangarShips.Where(x => x.stringID == shipID).First();
        
        if(equipmentType == EquipmentType.ForwardWeapon)
        {
            int availableIndex = 0;
            bool isAvailable = false;

            for (int i = 0; i < selectedShip.forwardWeapons.Count; i++)
            {
                if (selectedShip.forwardWeapons[i] == null && !isAvailable)
                {
                    availableIndex = i;
                    isAvailable = true;
                }
            }

            selectedShip.AssignWeapons(WeaponConfiguration.Forward, equipmentID, availableIndex);
        }

        if(equipmentType == EquipmentType.TurrentWeapon)
        {
            int availableIndex = 0;
            bool isAvailable = false;

            for (int i = 0; i < selectedShip.turrentWeapons.Count; i++)
            {
                if (selectedShip.forwardWeapons[i] == null && !isAvailable)
                {
                    availableIndex = i;
                    isAvailable = true;
                }
            }

            selectedShip.AssignWeapons(WeaponConfiguration.Forward, equipmentID, availableIndex);
        }


        return false;
    }


    public bool CheckSlotAvailability()
    {
        ShipInfo info = SessionData.instance.hangarCurrentSave.hangarShips.Where(x => x.stringID == this.shipID).First();

        if (equipmentType == EquipmentType.ForwardWeapon)
        {
            return info.forwardWeapons.Where(x => x == null).First() == null;
        } else
        {
            return info.turrentWeapons.Where(x => x == null).First() == null;
        }
    }

    public void DisplayForwardInventory()
    {
        equipmentType = EquipmentType.ForwardWeapon;
    }

    public void DisplayTurrentInventory()
    {
        equipmentType = EquipmentType.TurrentWeapon;
    }

    public string GetShipID()
    {
        return shipID;
    }
}

public enum EquipmentType
{
    ForwardWeapon,
    TurrentWeapon
}

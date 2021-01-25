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

    [Space]
    public List<GameObject> inventoryCells;

    public string shipID;

    public void PopulateInventoryList()
    {
        ClearInventoryList();

        GameObject cellPrefab = GameManager.Instance.uiSettings.prototypeEquipmentCell;

        Debug.Log("Has entered menu");
        List<ShipInfo> hangarShips = SessionData.instance.hangarCurrentSave.GetHangarShips();
        ShipInfo selectedShip = hangarShips.Where(x => x.stringID == shipID).First();

        Debug.Log(selectedShip.name);

        if (equipmentType == EquipmentType.ForwardWeapon)
        {
            PopulateEquipmentSlots(selectedShip, cellPrefab, EquipmentType.ForwardWeapon);

            /*foreach (WeaponInfo info in selectedShip.forwardWeapons)
            {
                GameObject spawnedInstance = Instantiate(cellPrefab, contentView.transform);
                IInventoryCell cellInterface = spawnedInstance.GetComponent<IInventoryCell>();
                cellInterface.SetData(info.stringID, info.name, null, info.price.ToString(), EquipmentType.ForwardWeapon);

                inventoryCells.Add(spawnedInstance);
            }*/
        }
    }



    //TODO: MUST CONVERT THE WEAPON INFO SYSTEMS TO USE STRING LITERAL



    private void PopulateEquipmentSlots(ShipInfo selectedShip, GameObject cellPrefab, EquipmentType equipmentType)
    {

        foreach (WeaponInfo info in selectedShip.forwardWeapons)
        {
            if (info.stringID == "")
            {
                GameObject emptyCell = GameManager.Instance.uiSettings.emptyListCell;
                GameObject spawnedEmpty = Instantiate(emptyCell, contentView.transform);
                inventoryCells.Add(emptyCell);
            }
            else
            {
                GameObject spawnedInstance = Instantiate(cellPrefab, contentView.transform);
                IInventoryCell cellInterface = spawnedInstance.GetComponent<IInventoryCell>();
                cellInterface.SetData(info.stringID, info.name, null, info.price.ToString(), equipmentType);

                inventoryCells.Add(spawnedInstance);
            }
        }
    }

    public void ClearInventoryList()
    {
        if (inventoryCells.Count == 0) return;

        foreach (GameObject cell in inventoryCells)
        {
            Destroy(cell);
        }
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

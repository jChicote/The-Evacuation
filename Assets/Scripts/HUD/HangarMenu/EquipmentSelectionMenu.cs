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
    bool AssignItem(string stringID);
    void RemoveItem(string stringID);
}

public interface ICheckShipSlot
{
    bool CheckSlotAvailability();
    bool CheckInEquipmentSlot(string equipmentID);
}

public interface IInfoPanel
{
    void SetInfoPanel(string equipmentID);

    void SetInfoPanel(WeaponType type, string name);
}

public interface IEquipmentMenu
{
    void InitialiseMenu();
    void PopulateInventoryList();
    EquipmentType GetCurrentType();
}


public class EquipmentSelectionMenu : MonoBehaviour, IIdentifyShip, IShipAssign, ICheckShipSlot, IInfoPanel, IEquipmentMenu
{
    public EquipmentType equipmentType;

    [Header("Content Attributes")]
    public Image shipImage;
    public GameObject contentView;
    public GameObject informationPanel;

    [Space]
    public List<GameObject> inventoryCells;

    public string shipID;
    public Sprite defaultSprite;

    // Sub Presenters
    private EquipmentCellPopulator cellPopulator;

    public void InitialiseMenu()
    {
        shipID = SessionData.instance.hangarCurrentSave.hangarShips[0].stringID;

        cellPopulator = gameObject.AddComponent<EquipmentCellPopulator>();
        cellPopulator.IntialisePopulator(this, contentView, this, this, this);
    }

    /// <summary>
    /// Called to the inventory list for UI and presentation.
    /// </summary>
    public void PopulateInventoryList()
    {
        ClearInventoryList();

        GameObject cellPrefab = GameManager.Instance.uiSettings.prototypeEquipmentCell;
        List<ShipInfo> hangarShips = SessionData.instance.hangarCurrentSave.GetHangarShips();
        //ShipInfo selectedShip = hangarShips.Where(x => x.stringID == shipID).First();
        ShipInfo selectedShip = hangarShips[0];

        if (equipmentType == EquipmentType.ForwardWeapon)
        {
            cellPopulator.PopulateEquipmentSlots(inventoryCells, selectedShip, cellPrefab, EquipmentType.ForwardWeapon);
            cellPopulator.CreateInventoryCell(cellPrefab, inventoryCells);
        }
        else if (equipmentType == EquipmentType.TurrentWeapon)
        {
            cellPopulator.PopulateEquipmentSlots(inventoryCells, selectedShip, cellPrefab, EquipmentType.TurrentWeapon);
            cellPopulator.CreateInventoryCell(cellPrefab, inventoryCells);
        }
    }

    /// <summary>
    /// Assigns / Attaches equipment item to the open equipment type and ship list.
    /// </summary>
    public bool AssignItem(string equipmentID)
    {
        // Get the selected ship
        List<ShipInfo> hangarShips = SessionData.instance.hangarCurrentSave.GetHangarShips();
        ShipInfo selectedShip = hangarShips.Where(x => x.stringID == shipID).First();
        
        if(equipmentType == EquipmentType.ForwardWeapon)
        {
            if (selectedShip.CheckIsFull(WeaponConfiguration.Forward)) return false;
            selectedShip.AssignWeapons(WeaponConfiguration.Forward, equipmentID);
        }

        if(equipmentType == EquipmentType.TurrentWeapon)
        {
            if (selectedShip.CheckIsFull(WeaponConfiguration.Turrent)) return false;
            selectedShip.AssignWeapons(WeaponConfiguration.Turrent, equipmentID);
        }

        ResetEquipmentList();
        return false;
    }

    /// <summary>
    /// Removes the specified equipment ID from the ship's info list based on set equipment type.
    /// </summary>
    public void RemoveItem(string equipmentID)
    {
        // Get the selected ship
        List<ShipInfo> hangarShips = SessionData.instance.hangarCurrentSave.GetHangarShips();
        ShipInfo selectedShip = hangarShips.Where(x => x.stringID == shipID).First();

        if (equipmentType == EquipmentType.ForwardWeapon)
        {
            selectedShip.RemoveWeapon(WeaponConfiguration.Forward, equipmentID);
        }

        if (equipmentType == EquipmentType.TurrentWeapon)
        {
            selectedShip.RemoveWeapon(WeaponConfiguration.Turrent, equipmentID);
        }

        ResetEquipmentList();
    }

    /// <summary>
    /// Returns true if the ship has an empty available equipment slot for population.
    /// </summary>
    public bool CheckSlotAvailability()
    {
        ShipInfo info = SessionData.instance.hangarCurrentSave.hangarShips.Where(x => x.stringID == this.shipID).First();

        if (equipmentType == EquipmentType.ForwardWeapon)
        {
            return !info.CheckIsFull(WeaponConfiguration.Forward); //TODO: Later collapse into info function only
        } else
        {
            return !info.CheckIsFull(WeaponConfiguration.Turrent);
        }
    }

    /// <summary>
    /// Returns true if specified equipment ID is found within the ship's weapons.
    /// </summary>
    public bool CheckInEquipmentSlot(string equipmentID)
    {
        ShipInfo info = SessionData.instance.hangarCurrentSave.hangarShips.Where(x => x.stringID == this.shipID).First();

        if (equipmentType == EquipmentType.ForwardWeapon)
        {
            if (info.forwardWeapons.Count == 0) return false;
            return info.forwardWeapons.Where(x => x == equipmentID).First() == equipmentID;
        }
        else
        {
            if (info.turrentWeapons.Count == 0) return false;
            return info.turrentWeapons.Where(x => x == equipmentID).First() == equipmentID;
        }
    }

    /// <summary>
    /// Sets the information on the information panel based on the information set on both the info and assets.
    /// </summary>
    public void SetInfoPanel(string equipmentID)
    {
        WeaponInfo info = SessionData.instance.GetWeaponItem(equipmentID);
        WeaponAsset asset = GameManager.Instance.weaponSettings.RetrieveFromSettings(info.weaponType, info.name);

        informationPanel.SetActive(true);
        InformationPanel infoPanel = informationPanel.GetComponent<InformationPanel>();
        infoPanel.SetPanelInfo(info.name, asset.description, defaultSprite);
    }

    public void SetInfoPanel(WeaponType type, string name)
    {

    }

    /// <summary>
    /// Empties the menu's inverntory list.
    /// </summary>
    public void ClearInventoryList()
    {
        if (inventoryCells.Count == 0) return;

        for (int i = 0; i < inventoryCells.Count; i++)
        {
            GameObject item = inventoryCells[i];
            inventoryCells[i] = null;
            Destroy(item);
        }

        inventoryCells.Clear();
    }

    /// <summary>
    /// Opens forward equipment list menu when triggered by button.
    /// </summary>
    public void OpenForwardEquipment()
    {
        if (equipmentType == EquipmentType.ForwardWeapon) return;

        equipmentType = EquipmentType.ForwardWeapon;
        ResetEquipmentList();
    }

    /// <summary>
    /// Opens turrent equipment list menu when triggered by button.
    /// </summary>
    public void OpenTurrentEquipment()
    {
        if (equipmentType == EquipmentType.TurrentWeapon) return;

        equipmentType = EquipmentType.TurrentWeapon;
        ResetEquipmentList();
    }

    /// <summary>
    /// Resets the equipment list.
    /// </summary>
    private void ResetEquipmentList()
    {
        ClearInventoryList();
        PopulateInventoryList();
    }

    /// <summary>
    /// Called to get the ship ID assigned to the open menu.
    /// </summary>
    public string GetShipID()
    {
        return shipID;
    }

    /// <summary>
    /// Called to get the equipment type currently set on the menu.
    /// </summary>
    public EquipmentType GetCurrentType()
    {
        return equipmentType;
    }
}

public enum EquipmentType
{
    ForwardWeapon,
    TurrentWeapon
}

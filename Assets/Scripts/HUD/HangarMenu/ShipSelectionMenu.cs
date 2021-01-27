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
}


public class ShipSelectionMenu : MonoBehaviour, IIdentifyShip, IShipAssign, ICheckShipSlot, IInfoPanel
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

    public void InitialiseMenu()
    {
        // Temporary for now
        shipID = SessionData.instance.hangarCurrentSave.hangarShips[0].stringID;
    }

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

            foreach (WeaponInfo info in SessionData.instance.hangarCurrentSave.hangarWeapons)
            {
                if (!info.isAttached)
                {
                    GameObject spawnedInstance = Instantiate(cellPrefab, contentView.transform);
                    IInventoryCell cellInterface = spawnedInstance.GetComponent<IInventoryCell>();
                    cellInterface.SetData(info.stringID, info.name, defaultSprite, info.price.ToString(), EquipmentType.ForwardWeapon, this);
                    IEquipmentCell equipmentInterface = spawnedInstance.GetComponent<IEquipmentCell>();
                    equipmentInterface.SetCell(this, this, false);

                    inventoryCells.Add(spawnedInstance);
                }
            }
        }
    }


    private void PopulateEquipmentSlots(ShipInfo selectedShip, GameObject cellPrefab, EquipmentType equipmentType)
    {

        foreach (string identifier in selectedShip.forwardWeapons)
        {
            if (identifier == "")
            {
                GameObject emptyCell = GameManager.Instance.uiSettings.emptyListCell;
                GameObject spawnedEmpty = Instantiate(emptyCell, contentView.transform);
                inventoryCells.Add(spawnedEmpty);
            }
            else
            {
                WeaponInfo info = SessionData.instance.GetWeaponItem(identifier);

                GameObject spawnedInstance = Instantiate(cellPrefab, contentView.transform);
                IInventoryCell cellInterface = spawnedInstance.GetComponent<IInventoryCell>();
                cellInterface.SetData(info.stringID, info.name, null, info.price.ToString(), equipmentType, this);
                IEquipmentCell equipmentInterface = spawnedInstance.GetComponent<IEquipmentCell>();
                equipmentInterface.SetCell(this, this, true);

                inventoryCells.Add(spawnedInstance);
            }
        }
    }

    public void ClearInventoryList()
    {
        if (inventoryCells.Count == 0) return;

        for (int i=0; i < inventoryCells.Count; i++)
        {
            GameObject item = inventoryCells[i];
            inventoryCells[i] = null;
            Destroy(item);
        }

        inventoryCells.Clear();
    }

    public bool AssignItem(string equipmentID)
    {
        // Get the selected ship
        List<ShipInfo> hangarShips = SessionData.instance.hangarCurrentSave.GetHangarShips();
        ShipInfo selectedShip = hangarShips.Where(x => x.stringID == shipID).First();
        
        if(equipmentType == EquipmentType.ForwardWeapon)
        {
            selectedShip.AssignWeapons(WeaponConfiguration.Forward, equipmentID);
        }

        if(equipmentType == EquipmentType.TurrentWeapon)
        {
            selectedShip.AssignWeapons(WeaponConfiguration.Turrent, equipmentID);
        }

        ClearInventoryList();
        PopulateInventoryList();


        return false;
    }

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

        ClearInventoryList();
        PopulateInventoryList();
    }

    public bool CheckSlotAvailability()
    {
        ShipInfo info = SessionData.instance.hangarCurrentSave.hangarShips.Where(x => x.stringID == this.shipID).First();

        if (equipmentType == EquipmentType.ForwardWeapon)
        {
            return info.forwardWeapons.Where(x => x == "").First() == "";
        } else
        {
            return true;
        }
    }

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
            return true;
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

    public void SetInfoPanel(string equipmentID)
    {
        WeaponInfo info = SessionData.instance.GetWeaponItem(equipmentID);
        WeaponAsset asset = GameManager.Instance.weaponSettings.RetrieveFromSettings(info.weaponType, equipmentID);

        InformationPanel infoPanel = informationPanel.GetComponent<InformationPanel>();
        infoPanel.SetPanelInfo(info.name, asset.description, null);
        informationPanel.SetActive(true);
    }
}

public enum EquipmentType
{
    ForwardWeapon,
    TurrentWeapon
}

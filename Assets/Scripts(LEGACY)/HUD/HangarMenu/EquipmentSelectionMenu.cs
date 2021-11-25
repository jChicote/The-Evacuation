using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Evacuation.Model;
using Evacuation.Actor;
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

    void SetInfoPanel(WeaponType type, string universalID);
}

public interface IEquipmentMenu
{
    void InitialiseMenu();
    void OpenMenu(string shipID);
    void PopulateInventoryList();
    EquipmentType GetCurrentType();
}

namespace Evacuation.UserInterface 
{
    public class EquipmentSelectionMenu : MonoBehaviour, IIdentifyShip, IShipAssign, ICheckShipSlot, IInfoPanel, IEquipmentMenu
    {
        [Header("Content Attributes")]
        public Image shipImage;
        public GameObject contentView;
        public GameObject informationPanel;
        public Button forwardButton;
        public Button turrentButton;

        [Space]
        public List<GameObject> inventoryCells;

        private EquipmentType equipmentType;
        private ShipDataServicer shipDataServicer;
        private string shipID;
        private IWeaponServicer weaponServicer;

        // Sub Presenters
        private EquipmentCellPopulator cellPopulator;

        public void InitialiseMenu()
        {
            cellPopulator = gameObject.AddComponent<EquipmentCellPopulator>();
            cellPopulator.IntialisePopulator(this, contentView, this, this, this);

            shipDataServicer = SessionData.instance.shipServicer;
            weaponServicer = SessionData.instance.weaponServicer.GetComponent<IWeaponServicer>();
        }

        public void OpenMenu(string shipID)
        {
            this.shipID = shipID;
            shipImage.sprite = GameManager.Instance.playerSettings.shipsList.Where(x => x.instanceID == shipID).First().image;
        }


        /// <summary>
        /// Called to the inventory list for UI and presentation.
        /// </summary>
        public void PopulateInventoryList()
        {
            ClearInventoryList();

            if (equipmentType == EquipmentType.ForwardWeapon)
            {
                ShowEquipment(EquipmentType.ForwardWeapon);
            }
            else if (equipmentType == EquipmentType.TurrentWeapon)
            {
                ShowEquipment(EquipmentType.TurrentWeapon);
            }
        }

        /// <summary>
        /// Called to show equipment through the sub presenter
        /// </summary>
        /// <param name="equipmentType"></param>
        private void ShowEquipment(EquipmentType equipmentType)
        {
            GameObject cellPrefab = GameManager.Instance.uiSettings.prototypeEquipmentCell;
            ShipInfo selectedShip = shipDataServicer.GetShipItem(shipID);

            cellPopulator.PopulateEquipmentSlots(inventoryCells, selectedShip, cellPrefab, equipmentType);
            cellPopulator.CreateInventoryCell(cellPrefab, inventoryCells);
        }

        /// <summary>
        /// Assigns / Attaches equipment item to the open equipment type and ship list.
        /// </summary>
        public bool AssignItem(string itemID)
        {
            ShipInfo selectedShip = shipDataServicer.GetShipItem(shipID);
            /*print("Has been encountered");
            print("Item ID: " + itemID);
            print(shipID);
            print(selectedShip);*/

            if (equipmentType == EquipmentType.ForwardWeapon)
            {
                if (selectedShip.CheckIsFull(WeaponConfiguration.Forward)) return false;
                selectedShip.AssignWeapons(WeaponConfiguration.Forward, itemID);
               // print("Has assigned weapon");
            }

            if (equipmentType == EquipmentType.TurrentWeapon)
            {
                if (selectedShip.CheckIsFull(WeaponConfiguration.Turrent)) return false;
                selectedShip.AssignWeapons(WeaponConfiguration.Turrent, itemID);
                //print("Has assigned weapon");
            }

            ResetEquipmentList();
            return false;
        }

        /// <summary>
        /// Removes the specified equipment ID from the ship's info list based on set equipment type.
        /// </summary>
        public void RemoveItem(string itemID)
        {
            ShipInfo selectedShip = shipDataServicer.GetShipItem(shipID);

            if (equipmentType == EquipmentType.ForwardWeapon)
            {
                selectedShip.RemoveWeapon(WeaponConfiguration.Forward, itemID);
            }

            if (equipmentType == EquipmentType.TurrentWeapon)
            {
                selectedShip.RemoveWeapon(WeaponConfiguration.Turrent, itemID);
            }

            ResetEquipmentList();
        }

        /// <summary>
        /// Returns true if the ship has an empty available equipment slot for population.
        /// </summary>
        public bool CheckSlotAvailability()
        {
            ShipInfo info = shipDataServicer.GetShipItem(shipID);

            if (equipmentType == EquipmentType.ForwardWeapon)
            {
                return !info.CheckIsFull(WeaponConfiguration.Forward); //TODO: Later collapse into info function only
            }
            else
            {
                return !info.CheckIsFull(WeaponConfiguration.Turrent);
            }
        }

        /// <summary>
        /// Returns true if specified equipment ID is found within the ship's weapons.
        /// </summary>
        public bool CheckInEquipmentSlot(string equipmentID)
        {
            ShipInfo info = shipDataServicer.GetShipItem(shipID);

            if (equipmentType == EquipmentType.ForwardWeapon)
            {
                if (info.fixedWeapons.Count == 0) return false;
                return info.fixedWeapons.Where(x => x == equipmentID).First() == equipmentID;
            }
            else
            {
                if (info.fixedWeapons.Count == 0) return false;
                return info.fixedWeapons.Where(x => x == equipmentID).First() == equipmentID;
            }
        }

        /// <summary>
        /// Sets the information on the information panel based on the information set on both the info and assets.
        /// </summary>
        public void SetInfoPanel(string equipmentID)
        {
            WeaponInfo info = weaponServicer.GetWeaponItem(equipmentID);
            WeaponAsset asset = GameManager.Instance.weaponSettings.RetrieveFromSettings(info.weaponType, info.globalID);

            informationPanel.SetActive(true);
            InformationPanel infoPanel = informationPanel.GetComponent<InformationPanel>();
            infoPanel.SetPanelInfo(info.name, asset.description, asset.spriteThumbnail);
        }

        public void SetInfoPanel(WeaponType type, string name)
        {
            // Currently Empty
        }

        /// <summary>
        /// Empties the menu's inverntory list.
        /// </summary>
        public void ClearInventoryList()
        {
            if (inventoryCells.Count == 0) return;

            GameObject itemToDelete;
            for (int i = 0; i < inventoryCells.Count; i++)
            {
                itemToDelete = inventoryCells[i];
                inventoryCells[i] = null;
                Destroy(itemToDelete);
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
            print("Encountered Reset");
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

        /// <summary>
        /// Ensures to clear the list before closing menu (prevent possible memory leaks or holding objects in memory after disuse).
        /// </summary>
        public void ExitingMenu()
        {
            ClearInventoryList();
        }
    }
}

public enum EquipmentType
{
    ForwardWeapon,
    TurrentWeapon
}

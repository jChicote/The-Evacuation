using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Actor;
using Evacuation.Model;

namespace Evacuation.UserInterface
{
    public class EquipmentCellPopulator : MonoBehaviour
    {
        // Fields
        private GameObject contentView;
        private ICheckShipSlot slotChecker;
        private IShipAssign assigner;
        private IInfoPanel infoPanel;
        private IEquipmentMenu equipmentMenu;
        private IWeaponServicer weaponServicer;

        public void IntialisePopulator(IEquipmentMenu equipmentMenu, GameObject contentView, ICheckShipSlot slotChecker, IShipAssign assigner, IInfoPanel infoPanel)
        {
            this.equipmentMenu = equipmentMenu;
            this.contentView = contentView;
            this.slotChecker = slotChecker;
            this.assigner = assigner;
            this.infoPanel = infoPanel;

            weaponServicer = SessionData.instance.weaponServicer.GetComponent<IWeaponServicer>();
        }

        /// <summary>
        /// Called to load equipment representation of ship's equipment loadout to UI.
        /// </summary>
        public void PopulateEquipmentSlots(List<GameObject> inventoryCells, ShipInfo selectedShip, GameObject cellPrefab, EquipmentType equipmentType)
        {
            switch (equipmentType)
            {
                case EquipmentType.ForwardWeapon:
                    foreach (string identifier in selectedShip.fixedWeapons)
                    {
                        SpawnEquipmentCell(identifier, cellPrefab, inventoryCells);
                    }
                    break;
                case EquipmentType.TurrentWeapon:
                    foreach (string identifier in selectedShip.turrentWeapons)
                    {
                        SpawnEquipmentCell(identifier, cellPrefab, inventoryCells);
                    }
                    break;
            }
        }


        /// <summary>
        /// Called to instantiate an inventory prototype cell to be added to the inventory cell list.
        /// </summary>
        public void CreateInventoryCell(GameObject cellPrefab, List<GameObject> inventoryCells)
        {
            foreach (WeaponInfo info in weaponServicer.GetHangarWeapons())
            {
                if (!info.isAttached)
                {
                    GameObject spawnedInstance = Instantiate(cellPrefab, contentView.transform);

                    IInventoryCell cellInterface = spawnedInstance.GetComponent<IInventoryCell>();
                    cellInterface.InitialiseCell(info);
                    cellInterface.PassInterfaces(infoPanel);
                    cellInterface.SetColor(GetWeaponCellColor(info.weaponType));

                    IUICellChecker equipmentInterface = spawnedInstance.GetComponent<IUICellChecker>();
                    equipmentInterface.SetCell(slotChecker, assigner, false);

                    inventoryCells.Add(spawnedInstance);
                }
            }
        }

        // TODO: get this to reference string type
        // TODO: this class is being repeated twice
        private Color GetWeaponCellColor(WeaponType type)
        {
            UISettings uiSetting = GameManager.Instance.uiSettings;

            switch (type)
            {
                case WeaponType.Turrent:
                    return uiSetting.GetSpecifiedColor(CellColor.Red);
                case WeaponType.Laser:
                    return uiSetting.GetSpecifiedColor(CellColor.Green);
                case WeaponType.Launcher:
                    return uiSetting.GetSpecifiedColor(CellColor.Blue);
            }

            return Color.gray;
        }

        /// <summary>
        /// Called to instantiate an equipment cell using the specified prefab.
        /// </summary>
        public void SpawnEquipmentCell(string identifier, GameObject cellPrefab, List<GameObject> inventoryCells)
        {
            if (identifier == "")
            {
                GameObject emptyCell = GameManager.Instance.uiSettings.emptyListCell;
                GameObject spawnedInstance = Instantiate(emptyCell, contentView.transform);
                inventoryCells.Add(spawnedInstance);
            }
            else
            {
                WeaponInfo info = weaponServicer.GetWeaponItem(identifier);
                GameObject spawnedInstance = Instantiate(cellPrefab, contentView.transform);

                IInventoryCell cellInterface = spawnedInstance.GetComponent<IInventoryCell>();
                cellInterface.InitialiseCell(info);
                cellInterface.PassInterfaces(infoPanel);
                //cellInterface.SetColor(GetWeaponCellColor(equipmentMenu.GetCurrentType())); DOES NOT WORK UNTIL EQUIPMENT TYPES ARE CONSIDERED.

                IUICellChecker equipmentInterface = spawnedInstance.GetComponent<IUICellChecker>();
                equipmentInterface.SetCell(slotChecker, assigner, true);

                inventoryCells.Add(spawnedInstance);
            }
        }

    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.UserInterface
{
    public class EquipmentCellPopulator : MonoBehaviour
    {
        private GameObject contentView;
        private ICheckShipSlot slotChecker;
        private IShipAssign assigner;
        private IInfoPanel infoPanel;
        private IEquipmentMenu equipmentMenu;

        public void IntialisePopulator(IEquipmentMenu equipmentMenu, GameObject contentView, ICheckShipSlot slotChecker, IShipAssign assigner, IInfoPanel infoPanel)
        {
            this.equipmentMenu = equipmentMenu;
            this.contentView = contentView;
            this.slotChecker = slotChecker;
            this.assigner = assigner;
            this.infoPanel = infoPanel;
        }


        /// <summary>
        /// Called to instantiate an inventory prototype cell to be added to the inventory cell list.
        /// </summary>
        public void CreateInventoryCell(GameObject cellPrefab, List<GameObject> inventoryCells)
        {
            foreach (WeaponInfo info in SessionData.instance.weaponServicer.GetHangarWeapons())
            {
                if (!info.isAttached)
                {
                    GameObject spawnedInstance = Instantiate(cellPrefab, contentView.transform);
                    IInventoryCell cellInterface = spawnedInstance.GetComponent<IInventoryCell>();
                    cellInterface.SetData(info.stringID, info.name, null, info.price.ToString(), EquipmentType.ForwardWeapon, infoPanel);
                    cellInterface.SetColor();
                    IEquipmentCell equipmentInterface = spawnedInstance.GetComponent<IEquipmentCell>();
                    equipmentInterface.SetCell(slotChecker, assigner, false);

                    inventoryCells.Add(spawnedInstance);
                }
            }
        }

        /// <summary>
        /// Called to load equipment representation of ship's equipment loadout to UI.
        /// </summary>
        public void PopulateEquipmentSlots(List<GameObject> inventoryCells, ShipInfo selectedShip, GameObject cellPrefab, EquipmentType equipmentType)
        {
            switch (equipmentType)
            {
                case EquipmentType.ForwardWeapon:
                    foreach (string identifier in selectedShip.forwardWeapons)
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
                WeaponInfo info = SessionData.instance.weaponServicer.GetWeaponItem(identifier);
                GameObject spawnedInstance = Instantiate(cellPrefab, contentView.transform);

                IInventoryCell cellInterface = spawnedInstance.GetComponent<IInventoryCell>();
                cellInterface.SetData(info.stringID, info.name, null, info.price.ToString(), equipmentMenu.GetCurrentType(), infoPanel);

                IEquipmentCell equipmentInterface = spawnedInstance.GetComponent<IEquipmentCell>();
                equipmentInterface.SetCell(slotChecker, assigner, true);

                inventoryCells.Add(spawnedInstance);
            }
        }

    }

}
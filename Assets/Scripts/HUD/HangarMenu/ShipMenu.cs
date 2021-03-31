using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Evacuation.UserInterface;

public interface IShipSelection
{
    void LoadMenuSelection(string shipID);
    void OpenMessagePopup(string shipID, ShipPopupOptions options);
}

public interface IShipMenu
{
    void InitialiseMenu(IHangarActions hangarActions);
    void OpenMenu();

}

namespace Evacuation.UserInterface
{
    public class ShipMenu : MonoBehaviour, IShipSelection, IShipMenu
    {
        [Header("Menu Attribute4s")]
        public GameObject selectionView;
        public GameObject contentView;
        public GameObject equipmentMenu;

        [Space]
        public GameObject messagePanel;
        [Space]
        public List<GameObject> cellList;

        // Interfaces
        private IHangarActions hangarActions;
        private IShipMessagePopup shipPopup;

        /// <summary>
        /// Initialises the ship's menu
        /// </summary>
        public void InitialiseMenu(IHangarActions hangarActions)
        {
            this.hangarActions = hangarActions;
            shipPopup = messagePanel.GetComponent<IShipMessagePopup>();
            shipPopup.InitialisePopup(this);
        }

        public void OpenMenu()
        {
            PopulateCellList();
        }

        /// <summary>
        /// Called to populate the cell list
        /// </summary>
        public void PopulateCellList()
        {
            ClearList();

            GameObject shipCellprefab = GameManager.Instance.uiSettings.prototypeShipCell;
            GameObject cellInstance;
            IShipCell shipCell;

            foreach (ShipAsset asset in GameManager.Instance.playerSettings.shipsList)
            {
                cellInstance = Instantiate(shipCellprefab, contentView.transform);
                shipCell = cellInstance.GetComponent<IShipCell>();
                shipCell.SetCell(this, hangarActions, asset.stringID, asset.image);
                cellList.Add(cellInstance);
            }
        }

        public void ClearList()
        {
            if (cellList.Count == 0) return;

            GameObject itemToDelete;
            for (int i = 0; i < cellList.Count; i++)
            {
                itemToDelete = cellList[i];
                cellList[i] = null;
                Destroy(itemToDelete);
            }

            cellList.Clear();
        }

        /// <summary>
        /// Loads the menu selection when a successful ship selection is made
        /// </summary>
        public void LoadMenuSelection(string shipID)
        {
            IEquipmentMenu menu = equipmentMenu.GetComponent<IEquipmentMenu>();
            equipmentMenu.SetActive(true);

            menu.OpenMenu(shipID);
        }

        /// <summary>
        /// Called to reveal message popup during transaction or locked content triggers made during interaction
        /// </summary>
        public void OpenMessagePopup(string shipID, ShipPopupOptions options)
        {
            messagePanel.SetActive(true);

            if (options == ShipPopupOptions.Locked)
            {
                shipPopup.ShowLockedPopup(shipID);
            }
            else if (options == ShipPopupOptions.Purchase)
            {
                shipPopup.ShowPurchasePopup(shipID);
            }
        }

        /// <summary>
        /// Ensures to clear the list before closing menu (prevent possible memory leaks or holding objects in memory after disuse).
        /// </summary>
        public void ExitingMenu()
        {
            ClearList();
        }
    }

}
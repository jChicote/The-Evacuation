using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Session;

public interface IShopMenu
{

}

public interface IShopTransaction
{
    void MakePurchase(string universalID, WeaponType type, int purchaseCost);
    void MakeSale(string universalID, int sellPrice);
}

namespace Evacuation.UserInterface
{
    public class ShopMenu : MonoBehaviour, IShopMenu, IShopTransaction
    {
        // This is the presenter class for the shop menu.
        // This utilises a MVP design for interfacing between data and UI. Session data (currently) handles most of the model functionality.
        //
        // This directly interfaces with the settings to populate the list
        // This should mainly interface with the scriptable settings for default items.

        [Header("Menu Attributes")]
        public GameObject shopPanel;
        public GameObject purchaseErrorPanel;

        [Space]
        public List<GameObject> shopCells;

        //Interfaces
        private IInfoPanel informationPanel;

        public void InitialiseMenu(IInfoPanel informationPanel)
        {
            this.informationPanel = informationPanel;
        }

        /// <summary>
        /// Populates the menu list.
        /// </summary>
        public void PopulateWeaponList()
        {
            ClearList();

            GameObject cellPrefab = GameManager.Instance.uiSettings.prototypeShopCell;
            GameObject spawnedCell = null;

            // Collapse into shop list cell
            foreach (WeaponAsset turrentAsset in GameManager.Instance.weaponSettings.turrentWeapons)
            {
                CreateCell(turrentAsset, cellPrefab, spawnedCell);
            }

            foreach (WeaponAsset laserAsset in GameManager.Instance.weaponSettings.laserWeapon)
            {
                CreateCell(laserAsset, cellPrefab, spawnedCell);
            }

            foreach (WeaponAsset launcherAsset in GameManager.Instance.weaponSettings.launcherWeapons)
            {
                CreateCell(launcherAsset, cellPrefab, spawnedCell);
            }
        }

        /// <summary>
        /// Updates the shop list when changes in the hangar or data are made.
        /// </summary>
        private void UpdateShopList()
        {
            IUpdateCell cellUpdator;

            foreach (GameObject cell in shopCells)
            {
                cellUpdator = cell.GetComponent<IUpdateCell>();
                cellUpdator.UpdateCell();
            }
        }

        /// <summary>
        ///  Responsible for creating template shop cells to be populated by weapon asset.
        /// </summary>
        private void CreateCell(WeaponAsset asset, GameObject cellPrefab, GameObject spawnedCell)
        {
            spawnedCell = Instantiate(cellPrefab, shopPanel.transform);
            IShopInsertData cellInserter = spawnedCell.GetComponent<IShopInsertData>();
            cellInserter.InsertInformation(asset);
            cellInserter.PassInterfaces(informationPanel, this);
            cellInserter.SetColor();
            shopCells.Add(spawnedCell);
        }

        private void ClearList()
        {
            if (shopCells.Count == 0) return;

            GameObject itemToDelete;
            for (int i = 0; i < shopCells.Count; i++)
            {
                itemToDelete = shopCells[i];
                shopCells[i] = null;
                Destroy(itemToDelete);
            }

            shopCells.Clear();
        }

        /// <summary>
        /// Processes purchases made in the shop.
        /// </summary>
        public void MakePurchase(string universalID, WeaponType type, int purchaseCost)
        {
            UserStatus userStatus = SessionData.instance.userStatus;

            if (userStatus.credits < purchaseCost)
            {
                purchaseErrorPanel.SetActive(true);
                return;
            }

            userStatus.credits -= purchaseCost;
            WeaponAsset asset = GameManager.Instance.weaponSettings.RetrieveFromSettings(type, universalID);
            SessionData.instance.weaponServicer.AddWeaponInstance(asset);
            SessionData.instance.OnUserTransaction.Invoke();

            UpdateShopList();
        }

        /// <summary>
        /// Processes sales made in the shop.
        /// </summary>
        public void MakeSale(string universalID, int sellPrice)
        {
            SessionData.instance.userStatus.credits += sellPrice;
            SessionData.instance.weaponServicer.RemoveWeaponInstance(universalID);
            SessionData.instance.OnUserTransaction.Invoke();

            UpdateShopList();
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

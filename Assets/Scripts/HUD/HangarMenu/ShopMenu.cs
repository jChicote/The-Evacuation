using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopMenu
{

}

public interface IShopTransaction
{
    void MakePurchase(string universalID, WeaponType type, int purchaseCost);
    void MakeSale(string universalID, int sellPrice);
}

namespace UserInterfaces
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

            // Collapse into shop list cell
            foreach (WeaponAsset turrentAsset in GameManager.Instance.weaponSettings.turrentWeapons)
            {
                CreateCell(turrentAsset);
            }

            foreach (WeaponAsset laserAsset in GameManager.Instance.weaponSettings.laserWeapon)
            {
                CreateCell(laserAsset);
            }

            foreach (WeaponAsset launcherAsset in GameManager.Instance.weaponSettings.launcherWeapons)
            {
                CreateCell(launcherAsset);
            }
        }

        /// <summary>
        ///  Responsible for creating template shop cells to be populated by weapon asset.
        /// </summary>
        private void CreateCell(WeaponAsset asset)
        {
            GameObject cellPrefab = GameManager.Instance.uiSettings.prototypeShopCell;
            GameObject spawnedCell;

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

            for (int i = 0; i < shopCells.Count; i++)
            {
                GameObject item = shopCells[i];
                shopCells[i] = null;
                Destroy(item);
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
            SessionData.instance.AddWeaponInstance(asset);
            SessionData.instance.OnUserTransaction.Invoke();
        }

        /// <summary>
        /// Processes sales made in the shop.
        /// </summary>
        public void MakeSale(string universalID, int sellPrice)
        {
            SessionData.instance.userStatus.credits += sellPrice;
            SessionData.instance.RemoveWeaponInstance(universalID);
            SessionData.instance.OnUserTransaction.Invoke();
        }
    }

}

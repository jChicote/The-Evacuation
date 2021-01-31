using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopMenu
{

}

public interface IShopTransaction
{
    void MakePurchase(string name, WeaponType type, int purchaseCost);
    void MakeSale(string name, int sellPrice);
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


        public GameObject shopPanel;
        public GameObject purchaseErrorPanel;

        public List<GameObject> shopCells;

        private IInfoPanel informationPanel;

        public void InitialiseMenu(IInfoPanel informationPanel)
        {
            this.informationPanel = informationPanel;
        }

        public void PopulateWeaponList()
        {
            ClearList();

            GameObject cellPrefab = GameManager.Instance.uiSettings.prototypeShopCell;
            GameObject spawnedCell;

            foreach (WeaponAsset asset in GameManager.Instance.weaponSettings.turrentWeapons)
            {
                spawnedCell = Instantiate(cellPrefab, shopPanel.transform);
                IShopInsertData cellInserter = spawnedCell.GetComponent<IShopInsertData>();
                cellInserter.InsertInformation(asset.name, WeaponType.Turrent, "stub description", asset.price, asset.weaponPrefab.GetComponent<IImageExtract>().ExtractImage());
                cellInserter.PassInterfaces(informationPanel, this);
                cellInserter.SetColor();
                shopCells.Add(spawnedCell);
            }

            foreach (WeaponAsset asset in GameManager.Instance.weaponSettings.laserWeapon)
            {
                spawnedCell = Instantiate(cellPrefab, shopPanel.transform);
                IShopInsertData cellInserter = spawnedCell.GetComponent<IShopInsertData>();
                cellInserter.InsertInformation(asset.name, WeaponType.Laser, "stub description", asset.price, asset.weaponPrefab.GetComponent<IImageExtract>().ExtractImage());
                cellInserter.PassInterfaces(informationPanel, this);
                cellInserter.SetColor();
                shopCells.Add(spawnedCell);
            }

            foreach (WeaponAsset asset in GameManager.Instance.weaponSettings.launcherWeapons)
            {
                spawnedCell = Instantiate(cellPrefab, shopPanel.transform);
                IShopInsertData cellInserter = spawnedCell.GetComponent<IShopInsertData>();
                cellInserter.InsertInformation(asset.name, WeaponType.Launcher, "stub description", asset.price, asset.weaponPrefab.GetComponent<IImageExtract>().ExtractImage());
                cellInserter.PassInterfaces(informationPanel, this);
                cellInserter.SetColor();
                shopCells.Add(spawnedCell);
            }
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

        public void PopulateShipList()
        {

        }

        /// <summary>
        /// Processes purchases made in the shop.
        /// </summary>
        public void MakePurchase(string name, WeaponType type, int purchaseCost)
        {
            UserStatus userStatus = SessionData.instance.userStatus;

            if (userStatus.credits < purchaseCost)
            {
                purchaseErrorPanel.SetActive(true);
                return;
            }

            userStatus.credits -= purchaseCost;
            WeaponAsset asset = GameManager.Instance.weaponSettings.RetrieveFromSettings(type, name);
            SessionData.instance.AddWeaponInstance(asset);
            SessionData.instance.OnUserTransaction.Invoke();
        }

        /// <summary>
        /// Processes sales made in the shop.
        /// </summary>
        public void MakeSale(string name, int sellPrice)
        {
            SessionData.instance.userStatus.credits += sellPrice;
            SessionData.instance.RemoveWeaponInstance(name);
            SessionData.instance.OnUserTransaction.Invoke();
        }
    }

}

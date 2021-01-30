using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopMenu
{

}

public class ShopMenu : MonoBehaviour, IShopMenu
{
    // This is the presenter class for the shop menu.
    // This utilises a MVP design for interfacing between data and UI. Session data (currently) handles most of the model functionality.
    //
    // This directly interfaces with the settings to populate the list
    // This should mainly interface with the scriptable settings for default items.


    public GameObject shopPanel;

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
            cellInserter.InsertInformation(asset.name, WeaponType.Turrent, "stub description", asset.price, 1, asset.weaponPrefab.GetComponent<IImageExtract>().ExtractImage(), informationPanel);
            cellInserter.SetColor();
            shopCells.Add(spawnedCell);
        }

        foreach (WeaponAsset asset in GameManager.Instance.weaponSettings.laserWeapon)
        {
            spawnedCell = Instantiate(cellPrefab, shopPanel.transform);
            IShopInsertData cellInserter = spawnedCell.GetComponent<IShopInsertData>();
            cellInserter.InsertInformation(asset.name, WeaponType.Laser, "stub description", asset.price, 1, asset.weaponPrefab.GetComponent<IImageExtract>().ExtractImage(), informationPanel);
            cellInserter.SetColor();
            shopCells.Add(spawnedCell);
        }

        foreach (WeaponAsset asset in GameManager.Instance.weaponSettings.launcherWeapons)
        {
            spawnedCell = Instantiate(cellPrefab, shopPanel.transform);
            IShopInsertData cellInserter = spawnedCell.GetComponent<IShopInsertData>();
            cellInserter.InsertInformation(asset.name, WeaponType.Launcher, "stub description", asset.price, 1, asset.weaponPrefab.GetComponent<IImageExtract>().ExtractImage(), informationPanel);
            cellInserter.SetColor();
            shopCells.Add(spawnedCell);
        }
    }

    public int CheckInventoryCount(string assetID)
    {


        return 0;
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
}

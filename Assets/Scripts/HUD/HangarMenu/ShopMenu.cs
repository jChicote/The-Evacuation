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

    [Header("Test Items")]
    public WeaponAsset[] testWeaponsList;

    public List<GameObject> shopCells;

    public void InitialiseMenu()
    {

    }

    public void PopulateWeaponList()
    {
        GameObject cellPrefab = GameManager.Instance.uiSettings.prototypeShopCell;
        GameObject spawnedCell;

        foreach (WeaponAsset asset in GameManager.Instance.weaponSettings.turrentWeapons)
        {
            spawnedCell = Instantiate(cellPrefab, shopPanel.transform);
            IShopInsertData cellInserter = spawnedCell.GetComponent<IShopInsertData>();
            cellInserter.InsertInformation(asset.name, "stub description", asset.price, 1, asset.weaponPrefab.GetComponent<IImageExtract>().ExtractImage());
            shopCells.Add(spawnedCell);
        }

        foreach (WeaponAsset asset in GameManager.Instance.weaponSettings.laserWeapon)
        {
            spawnedCell = Instantiate(cellPrefab, shopPanel.transform);
            IShopInsertData cellInserter = spawnedCell.GetComponent<IShopInsertData>();
            cellInserter.InsertInformation(asset.name, "stub description", asset.price, 1, asset.weaponPrefab.GetComponent<IImageExtract>().ExtractImage());
            shopCells.Add(spawnedCell);
        }

        foreach (WeaponAsset asset in GameManager.Instance.weaponSettings.launcherWeapons)
        {
            spawnedCell = Instantiate(cellPrefab, shopPanel.transform);
            IShopInsertData cellInserter = spawnedCell.GetComponent<IShopInsertData>();
            cellInserter.InsertInformation(asset.name, "stub description", asset.price, 1, asset.weaponPrefab.GetComponent<IImageExtract>().ExtractImage());
            shopCells.Add(spawnedCell);
        }
    }

    private int CheckInventoryCount(string assetID)
    {


        return 0;
    }

    public void PopulateShipList()
    {

    }
}

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

    public void InitialiseMenu()
    {

    }

    public void PopulateWeaponList()
    {
        GameObject cellPrefab = GameManager.Instance.uiSettings.prototypeShopCell;

        foreach (WeaponAsset info in testWeaponsList)
        {
            Debug.Log("Instances created");
            GameObject spawnedInstance = Instantiate(cellPrefab, shopPanel.transform);
            IShopInsertData cellInserter = spawnedInstance.GetComponent<IShopInsertData>();
            cellInserter.InsertInformation(info.name, "stub description", info.price, 1, info.weaponPrefab.GetComponent<IImageExtract>().ExtractImage());
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

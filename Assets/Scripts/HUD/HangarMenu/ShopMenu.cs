using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public GameObject shopPanel;

    [Header("Test Items")]
    public WeaponInfo[] testWeaponsList;

    public void PopulateWeaponList()
    {
        GameObject cellPrefab = GameManager.Instance.uiSettings.prototypeShopCell;

        foreach (WeaponInfo info in testWeaponsList)
        {
            Debug.Log("Instances created");
            GameObject spawnedInstance = Instantiate(cellPrefab, shopPanel.transform);
            IShopInsertData cellInserter = spawnedInstance.GetComponent<IShopInsertData>();
            cellInserter.InsertInformation(info.name, "stub description", info.weaponData.price, 1, info.weaponPrefab.GetComponent<IImageExtract>().ExtractImage());
        }
    }

    public void PopulateShipList()
    {

    }
}

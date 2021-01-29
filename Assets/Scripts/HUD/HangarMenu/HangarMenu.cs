using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangarMenu : MonoBehaviour, IInfoPanel
{
    public GameObject shopMenu;
    public GameObject inventoryMenu;
    public GameObject shipMenu;
    public GameObject equipmentMenu;

    [Space]
    public GameObject informationPanel;

    public void InitialiseHangar()
    {
        IEquipmentMenu equipment = equipmentMenu.GetComponent<IEquipmentMenu>();
        equipment.InitialiseMenu();

        ShopMenu shop = shopMenu.GetComponent<ShopMenu>();
        shop.InitialiseMenu(this);
    }

    public void RevealInventory()
    {
        shipMenu.SetActive(false);
        shopMenu.SetActive(false);
        equipmentMenu.SetActive(false);

        inventoryMenu.SetActive(true);
    }

    public void RevealShop()
    {
        shipMenu.SetActive(false);
        inventoryMenu.SetActive(false);
        equipmentMenu.SetActive(false);

        shopMenu.SetActive(true);

        ShopMenu menu = shopMenu.GetComponent<ShopMenu>();
        menu.PopulateWeaponList();
    }

    public void RevealShips()
    {
        shopMenu.SetActive(false);
        inventoryMenu.SetActive(false);
        equipmentMenu.SetActive(false);

        shipMenu.SetActive(true);

        IEquipmentMenu menu = shipMenu.GetComponent<IEquipmentMenu>();
        menu.PopulateInventoryList();
    }

    public void RevealEquipmentMenu()
    {
        shipMenu.SetActive(false);
        inventoryMenu.SetActive(false);
        shopMenu.SetActive(false);

        equipmentMenu.SetActive(true);

        IEquipmentMenu menu = equipmentMenu.GetComponent<IEquipmentMenu>();
       // menu.InitialiseMenu();
        menu.PopulateInventoryList();
    }

    /// <summary>
    /// Sets the information on the information panel based on the information set on both the info and assets.
    /// </summary>
    public void SetInfoPanel(WeaponType type, string name)
    {
        WeaponAsset asset = GameManager.Instance.weaponSettings.RetrieveFromSettings(type, name);

        informationPanel.SetActive(true);
        InformationPanel infoPanel = informationPanel.GetComponent<InformationPanel>();
        infoPanel.SetPanelInfo(name, asset.description, null);
    }

    /// <summary>
    /// Sets the information on the information panel based on the information set on both the info and assets.
    /// </summary>
    public void SetInfoPanel(string equipmentID)
    {
        WeaponInfo info = SessionData.instance.GetWeaponItem(equipmentID);
        WeaponAsset asset = GameManager.Instance.weaponSettings.RetrieveFromSettings(info.weaponType, info.name);

        informationPanel.SetActive(true);
        InformationPanel infoPanel = informationPanel.GetComponent<InformationPanel>();
        infoPanel.SetPanelInfo(info.name, asset.description, null);
    }

    public void HideHangarMenu()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangarMenu : MonoBehaviour
{
    public GameObject shopMenu;
    public GameObject inventoryMenu;
    public GameObject shipMenu;

    public void RevealInventory()
    {
        shipMenu.SetActive(false);
        shopMenu.SetActive(false);
        inventoryMenu.SetActive(true);
    }

    public void RevealShop()
    {
        shipMenu.SetActive(false);
        inventoryMenu.SetActive(false);
        shopMenu.SetActive(true);

        ShopMenu menu = shopMenu.GetComponent<ShopMenu>();
        menu.PopulateWeaponList();
    }

    public void RevealShips()
    {
        shopMenu.SetActive(false);
        inventoryMenu.SetActive(false);
        shipMenu.SetActive(true);
    }
}

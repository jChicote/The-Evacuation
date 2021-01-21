using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangarMenu : MonoBehaviour
{
    public GameObject shopMenu;
    public GameObject inventoryMenu;

    public void RevealInventory()
    {
        shopMenu.SetActive(false);
        inventoryMenu.SetActive(true);
    }

    public void RevealShop()
    {
        inventoryMenu.SetActive(false);
        shopMenu.SetActive(true);


        ShopMenu menu = shopMenu.GetComponent<ShopMenu>();
        menu.PopulateWeaponList();
    }
}

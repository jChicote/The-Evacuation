using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public interface IShopInsertData
{
    void InsertInformation(string name, string description, float price, int inventoryCount, Sprite thumbnail);
}

public class ShopListCell : MonoBehaviour, IShopInsertData
{
    [Header("Cell Information")]
    public TextMeshProUGUI cellTitle;
    public TextMeshProUGUI itemPrice;
    public TextMeshProUGUI availableCount;
    public Image cellImage;

    [Header("Action UI")]
    public GameObject actionGroup;
    public Button informationButton;
    public Button purchaseButton;
    public Button sellButton;

    // Collapse info
    public Sprite thumbnail;
    public string name;
    public float price;
    public int inventoryCount;

    public string description;

    public void InsertInformation(string name, string description, float price, int inventoryCount, Sprite thumbnail)
    {
        this.name = name;
        this.description = description;
        this.price = price;
        this.inventoryCount = inventoryCount;
        this.thumbnail = thumbnail;

        PopulateCell();
    }

    private void PopulateCell()
    {
        cellTitle.text = name;
        itemPrice.text = "$" + price;
        availableCount.text = "Inventory: " + inventoryCount;
        cellImage.sprite = thumbnail;
    }

    public void SellItem()
    {
        Debug.Log("Sold Item");
    }

    public void PurchaseItem()
    {
        Debug.Log("Purchased Item");
    }
}

public struct CellInformation
{
    public Image thumbnail;
    public string name;
    public float price;
    public int inventoryCount;

    public string description;
}

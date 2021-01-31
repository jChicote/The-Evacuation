using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public interface IShopInsertData
{
    void InsertInformation(string name, WeaponType type, string description, int price, Sprite thumbnail);
    void PassInterfaces(IInfoPanel infoPanel, IShopTransaction shopTransaction);
    void SetColor();
}

namespace UserInterfaces
{
    public class ShopListCell : MonoBehaviour, IShopInsertData
    {
        [Header("Cell Information")]
        public TextMeshProUGUI cellTitle;
        public TextMeshProUGUI itemPrice;
        public TextMeshProUGUI availableCount;
        public Image cellImage;
        public Image cellBackgroundImage;

        [Header("Action UI")]
        public GameObject actionGroup;
        public Button informationButton;
        public Button purchaseButton;
        public Button sellButton;

        // Collapse info
        public Sprite thumbnail;
        public WeaponType type;
        public string name;
        public int price;
        public int inventoryCount;
        private bool isActionsVisible = false;

        private IInfoPanel informationPanel;
        private IShopTransaction shopTransaction;

        public string description;

        public void InsertInformation(string name, WeaponType type, string description, int price, Sprite thumbnail)
        {
            this.name = name;
            this.type = type;
            this.description = description;
            this.price = price;
            this.thumbnail = thumbnail;

            PopulateCell();
        }

        public void PassInterfaces(IInfoPanel infoPanel, IShopTransaction shopTransaction)
        {
            this.informationPanel = infoPanel;
            this.shopTransaction = shopTransaction;
        }

        public void SetColor()
        {
            Color cellColor;

            switch (type)
            {
                case WeaponType.Turrent:
                    cellColor = GameManager.Instance.uiSettings.GetSpecifiedColor(CellColor.Red);
                    cellBackgroundImage.color = cellColor;
                    break;
                case WeaponType.Laser:
                    cellColor = GameManager.Instance.uiSettings.GetSpecifiedColor(CellColor.Green);
                    cellBackgroundImage.color = cellColor;
                    break;
                case WeaponType.Launcher:
                    cellColor = GameManager.Instance.uiSettings.GetSpecifiedColor(CellColor.Blue);
                    cellBackgroundImage.color = cellColor;
                    break;
            }
        }

        private void PopulateCell()
        {
            cellTitle.text = name;
            itemPrice.text = "$" + price;
            availableCount.text = "Inventory: " + SessionData.instance.GetWeaponInstanceCount(name);
            cellImage.sprite = thumbnail;
        }

        public void SellItem()
        {
            shopTransaction.MakeSale(name, price);
        }

        public void PurchaseItem()
        {
            shopTransaction.MakePurchase(name, type, price);
        }

        public void RevealActionGroup()
        {
            isActionsVisible = !isActionsVisible;
            actionGroup.SetActive(isActionsVisible);
        }

        public void RevealInformation()
        {
            Debug.Log(informationPanel);
            informationPanel.SetInfoPanel(this.type, this.name);
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

}
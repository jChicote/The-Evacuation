using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public interface IShopInsertData
{
    void InsertInformation(WeaponAsset asset);
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

        // cell characteristics
        public Sprite thumbnail;
        public WeaponType type;
        public string universalID;
        public string name;
        public int price;
        public int inventoryCount;
        private bool isActionsVisible = false;

        private IInfoPanel informationPanel;
        private IShopTransaction shopTransaction;

        public string description;

        public void InsertInformation(WeaponAsset asset)
        {
            this.name = asset.name;
            this.universalID = asset.universalID;
            this.type = asset.defaultData.weaponType;
            this.description = asset.description;
            this.price = asset.price;
            this.thumbnail = asset.weaponPrefab.GetComponent<IImageExtract>().ExtractImage();

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
            availableCount.text = "Inventory: " + SessionData.instance.GetWeaponInstanceCount(universalID);
            cellImage.sprite = thumbnail;
        }

        public void SellItem()
        {
            shopTransaction.MakeSale(universalID, price);
        }

        public void PurchaseItem()
        {
            shopTransaction.MakePurchase(universalID, type, price);
        }

        public void RevealActionGroup()
        {
            isActionsVisible = !isActionsVisible;
            actionGroup.SetActive(isActionsVisible);
        }

        public void RevealInformation()
        {
            Debug.Log(informationPanel);
            informationPanel.SetInfoPanel(this.type, this.universalID);
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
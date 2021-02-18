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

namespace UserInterface
{
    public interface IUpdateCell
    {
        /// <summary>
        /// Called when the cell information needs to be updated if there is a change in the list.
        /// </summary>
        void UpdateCell();
    }

    public class ShopListCell : MonoBehaviour, IShopInsertData, IUpdateCell
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

        // Cell characteristics
        public WeaponType type;
        public string universalID;
        public int price;
        private bool isActionsVisible = false;

        // Interfaces
        private IInfoPanel informationPanel;
        private IShopTransaction shopTransaction;

        /// <summary>
        /// Inserts information to the cell.
        /// </summary>
        public void InsertInformation(WeaponAsset asset)
        {
            this.universalID = asset.universalID;
            this.type = asset.defaultData.weaponType;
            this.price = asset.price;

            cellTitle.text = asset.name;
            itemPrice.text = "$" + price;
            availableCount.text = "Inventory: " + SessionData.instance.weaponServicer.GetAvailableWeaponInstanceCount(universalID);
            cellImage.sprite = asset.weaponPrefab.GetComponent<IImageExtract>().ExtractImage();
        }

        /// <summary>
        /// Called when the cell information needs to be updated if there is a change in the list.
        /// </summary>
        public void UpdateCell()
        {
            availableCount.text = "Inventory: " + SessionData.instance.weaponServicer.GetAvailableWeaponInstanceCount(universalID);
        }

        /// <summary>
        /// Passes interfaces used for the cell.
        /// </summary>
        public void PassInterfaces(IInfoPanel infoPanel, IShopTransaction shopTransaction)
        {
            this.informationPanel = infoPanel;
            this.shopTransaction = shopTransaction;
        }

        /// <summary>
        /// Sets the appropriate color for the weapon type of this weapon cell.
        /// </summary>
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

        /// <summary>
        /// Triggers the menu call for transaction of the weapon from the hangar.
        /// </summary>
        public void SellItem()
        {
            shopTransaction.MakeSale(universalID, price);
        }

        /// <summary>
        /// Called to trigger weapon purchase in the shop's transactor.
        /// </summary>
        public void PurchaseItem()
        {
            shopTransaction.MakePurchase(universalID, type, price);
        }

        /// <summary>
        /// Reveals the action group button when cell is clicked or triggered.
        /// </summary>
        public void RevealActionGroup()
        {
            isActionsVisible = !isActionsVisible;
            actionGroup.SetActive(isActionsVisible);
        }

        /// <summary>
        /// Reveals the information panel to present the information about this cell.
        /// </summary>
        public void RevealInformation()
        {
            informationPanel.SetInfoPanel(this.type, this.universalID);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Evacuation.Model;

public interface IShopInsertData
{
    void InsertInformation(WeaponAsset asset);
    void PassInterfaces(IInfoPanel infoPanel, IShopTransaction shopTransaction);
    void SetColor();
}

namespace Evacuation.UserInterface
{
    public interface IShopEquipmentCell : IEquipmentCells
    {
        void InitialiseCell(string itemID, WeaponAsset weaponAsset);
        void PassInterfaces(IInfoPanel infoPanel, IShopTransaction shopTransaction);
    }

    public class ShopListCell : BaseEquipmentCell, IShopEquipmentCell
    {
        public TextMeshProUGUI availableCount;

        [Header("Action UI")]
        public Button informationButton;
        public Button purchaseButton;
        public Button sellButton;

        // Cell characteristics
        public WeaponType type;

        // Interfaces
        private IShopTransaction shopTransaction;
        private IWeaponServicer weaponServicer;


        public void InitialiseCell(string itemID, WeaponAsset weaponAsset)
        {
            weaponServicer = SessionData.instance.weaponServicer.GetComponent<IWeaponServicer>();

            this.instanceID = itemID;
            this.type = weaponAsset.defaultData.weaponType;
            this.price = weaponAsset.price;

            cellTitle.text = weaponAsset.name;
            itemPrice.text = "$" + price;
            availableCount.text = "Inventory: " + weaponServicer.GetAvailableWeaponOccuranceCount(itemID);
            cellImage.sprite = weaponAsset.weaponPrefab.GetComponent<IImageExtract>().ExtractImage();
        }

        /*
         *  This is for future use for equipment assets
         * 
         * public override void InitialiseCell(string itemID, EquipmentAseset weaponAsset)
        {

        }
        */

        /// <summary>
        /// Called when the cell information needs to be updated if there is a change in the list.
        /// </summary>
        public override void UpdateCell()
        {
            availableCount.text = "Inventory: " + weaponServicer.GetAvailableWeaponOccuranceCount(instanceID);
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
        /// Triggers the menu call for transaction of the weapon from the hangar.
        /// </summary>
        public void SellItem()
        {
            print(instanceID);
            shopTransaction.MakeSale(instanceID, price);
        }

        /// <summary>
        /// Called to trigger weapon purchase in the shop's transactor.
        /// </summary>
        public void PurchaseItem()
        {
            shopTransaction.MakePurchase(instanceID, type, price);
        }

        /// <summary>
        /// Reveals the information panel to present the information about this cell.
        /// </summary>
        public override void RevealInformation()
        {
            informationPanel.SetInfoPanel(this.type, this.instanceID);
        }
    }
}
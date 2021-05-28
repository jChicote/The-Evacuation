using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Evacuation.Actor;
using Evacuation.Model;

public interface IShipMessagePopup
{
    void InitialisePopup(IShipMenu shipMenu);
    void ShowPurchasePopup(string shipID);
    void ShowLockedPopup(string shipID);
}

namespace Evacuation.UserInterface
{
    /// <summary>
    /// Responsible for handling the popup actions associated with the ship menu.
    /// </summary>
    public class ShipErrorPopup : MonoBehaviour, IShipMessagePopup
    {
        [Header("Purchase Popup Attributes")]
        public GameObject purchasePopup;
        public TextMeshProUGUI purchasedName;
        public TextMeshProUGUI purchaseDescription;

        [Header("Locked Popup Attributes")]
        public GameObject lockedPopup;
        public TextMeshProUGUI lockedName;
        public TextMeshProUGUI lockedDescription;

        private string shipID;
        private IShipMenu shipMenu;

        public void InitialisePopup(IShipMenu shipMenu)
        {
            this.shipMenu = shipMenu;
        }

        public void HideMessageBox()
        {
            lockedPopup.SetActive(false);
            purchasePopup.SetActive(false);
            gameObject.SetActive(false);
        }

        public void BuyShip()
        {
            ShipInfo info = SessionData.instance.shipServicer.GetShipItem(shipID);
            info.isUnlocked = true;
            HideMessageBox();
            shipMenu.OpenMenu();
        }

        public void ShowLockedPopup(string shipID)
        {
            this.shipID = shipID;
            lockedPopup.SetActive(true);

            ShipAsset asset = GameManager.Instance.playerSettings.shipsList.Where(x => x.instanceID == shipID).First();

            lockedName.text = asset.name;
            lockedDescription.text = "You need to be at level " + asset.requiredLevel + " in order to unlock this ship.";
        }

        public void ShowPurchasePopup(string shipID)
        {
            this.shipID = shipID;
            purchasePopup.SetActive(true);

            ShipAsset asset = GameManager.Instance.playerSettings.shipsList.Where(x => x.instanceID == shipID).First();

            purchasedName.text = asset.name;
            purchaseDescription.text = "Want to purchase this ship for $" + asset.price + "?";
        }
    }

}
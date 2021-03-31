using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public interface IShipCell
{
    void SetCell(IShipSelection shipSelector, IHangarActions hangarActions, string stringID, Sprite image);

}

namespace Evacuation.UserInterface
{
    public class ShipListCell : MonoBehaviour, IShipCell
    {
        [Header("Cell Attributes")]
        public Image cellThumbnail;

        private string stringID;
        private Sprite imageThumbnail;
        private IShipSelection shipSelector;
        private IHangarActions hangarActions;

        public void SetCell(IShipSelection shipSelector, IHangarActions hangarActions, string stringID, Sprite image)
        {
            this.shipSelector = shipSelector;
            this.hangarActions = hangarActions;
            this.stringID = stringID;
            this.imageThumbnail = image;

            PopulateCell();
        }

        private void PopulateCell()
        {
            ShipInfo info = SessionData.instance.shipServicer.GetShipItem(stringID);
            if(info.isUnlocked)
            {
                cellThumbnail.sprite = imageThumbnail;
            }
        }

        public void OpenSelectedShip()
        {
            ShipInfo info = SessionData.instance.shipServicer.GetShipItem(stringID);
            ShipAsset asset = GameManager.Instance.playerSettings.shipsList.Where(x => x.stringID == stringID).First();
            if (info.isUnlocked)
            {
                shipSelector.LoadMenuSelection(stringID);
                hangarActions.RevealEquipmentMenu();
                return;
            }

            //Revealed when requirement not achieved
            if (SessionData.instance.userStatus.userLevel < asset.requiredLevel)
            {
                shipSelector.OpenMessagePopup(stringID, ShipPopupOptions.Locked);
                return;
            }
            else if (!info.isUnlocked)
            {
                shipSelector.OpenMessagePopup(stringID, ShipPopupOptions.Purchase);
            }
        }

    }

    public enum ShipPopupOptions
    {
        Purchase,
        Locked
    }
}
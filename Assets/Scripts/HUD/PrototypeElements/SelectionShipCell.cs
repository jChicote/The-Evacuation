using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Evacuation.Session;
using Evacuation.Actor;

namespace Evacuation.UserInterface
{
    public interface ISelectionShipCell
    {
        void SetCell(string shipID, Sprite spriteThumbnail);
    }

    public class SelectionShipCell : MonoBehaviour, ISelectionShipCell
    {
        [Header("Cell Attributes")]
        public Image cellBackground;
        public Image cellThumbnail;
        public Button cellButton;

        private string shipID;
        private bool isUnlocked;
        private Sprite spriteThumbnail;

        public void SetCell(string shipID, Sprite spriteThumbnail)
        {
            this.shipID = shipID;
            SessionData sessionData = SessionData.instance;
            ShipInfo info = sessionData.shipServicer.GetShipItem(shipID);

            this.isUnlocked = info.isUnlocked;
            SetThumbnail(spriteThumbnail);
        }

        private void SetThumbnail(Sprite spriteThumbnail)
        {
            if(isUnlocked)
            {
                cellThumbnail.sprite = spriteThumbnail;
            }
        }

        public void OnTapSelection()
        {
            if(isUnlocked)
            {
                TriggerSelectSound();
                LoadSelectionToData();
                ChangeSelectionColor();
            }
            else
            {
                TriggerSelectSound();
            }
        }

        private void TriggerSelectSound()
        {
            //Debug.Log("Sound Triggered");
        }

        private void LoadSelectionToData()
        {
            ShipInfo info = SessionData.instance.shipServicer.GetShipItem(shipID);
            SessionData.instance.selectedShip = info;
        }

        private void ChangeSelectionColor()
        {
            cellBackground.color = GameManager.Instance.uiSettings.greenColor;
        }
    }
}

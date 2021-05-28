using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Evacuation.Model;


namespace Evacuation.UserInterface
{
    public interface IInventoryCell : IEquipmentCells
    {
        void InitialiseCell(WeaponInfo itemInfo);
        //void SetData(string stringID, string title, Sprite cellThumbnail, string cellPrice, EquipmentType type, IInfoPanel panel);
        void PassInterfaces(IInfoPanel infoPanel);
    }

    public abstract class InventoryListCell : BaseEquipmentCell, IInventoryCell
    {
        [Header("Cell Attributes")]
        public TextMeshProUGUI cellPrice;

        // Information view for item listing
        protected GameObject informationView;
        //protected EquipmentType equipmentType;
        //protected IInfoPanel infoPanelInterface;

        public virtual void InitialiseCell(WeaponInfo itemInfo)
        {
            this.instanceID = itemInfo.instanceID;
            this.cellTitle.text = itemInfo.name;
            //this.cellImage.sprite = itemInfo.im;
            this.cellPrice.text = itemInfo.price.ToString();
        }

        /*public void SetData(string stringID, string title, Sprite cellThumbnail, string cellPrice, EquipmentType type, IInfoPanel panel)
        {
            this.equipmentID = stringID;
            this.cellTitle.text = title;
            this.cellImage.sprite = cellThumbnail;
            this.cellPrice.text = cellPrice;
            this.equipmentType = type;
            this.infoPanelInterface = panel;
        }*/

        public virtual void PassInterfaces(IInfoPanel infoPanel)
        {
            this.informationPanel = infoPanel;
        }


        public override void RevealInformation()
        {
            informationPanel.SetInfoPanel(instanceID);
        }

        /*public void SetColor()
        {
            WeaponType type = SessionData.instance.weaponServicer.GetWeaponItem(equipmentID).weaponType;
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
        }*/
    }

}
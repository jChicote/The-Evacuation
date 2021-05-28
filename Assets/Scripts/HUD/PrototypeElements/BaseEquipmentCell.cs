using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Evacuation.UserInterface
{
    public interface IUICellChecker
    {
        void SetCell(ICheckShipSlot slotChecker, IShipAssign assigner, bool isAttached);
    }

    public interface IEquipmentCells
    {
        void InitialiseCell(string itemID);
        void UpdateCell();
        void SetColor(Color newColor);
        void RevealActionGroup();
        void RevealInformation();
    }

    public class BaseEquipmentCell : MonoBehaviour, IEquipmentCells
    {
        // Inspector Accessible Fields
        [Header("Cell UI ATtributes")]
        [SerializeField] protected GameObject actionGroup;
        [SerializeField] protected TextMeshProUGUI cellTitle;
        [SerializeField] protected TextMeshProUGUI itemPrice;
        [SerializeField] protected Image cellImage;
        [SerializeField] protected Image cellBackgroundImage;

        // Fields
        [Space]
        protected bool isActionsVisible = false;
        public string instanceID;
        public int price;

        // Interfaces
        protected IInfoPanel informationPanel;

        public virtual void InitialiseCell(string itemID)
        {
            this.instanceID = itemID;
        }

        public virtual void UpdateCell() { }

        public virtual void SetColor(Color newColor)
        {
            cellBackgroundImage.color = newColor;
        }

        public virtual void RevealActionGroup()
        {
            isActionsVisible = !isActionsVisible;
            actionGroup.SetActive(isActionsVisible);
        }

        public virtual void RevealInformation() { }
    }
}

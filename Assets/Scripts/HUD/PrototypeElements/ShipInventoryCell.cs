using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IEquipmentCell
{
    void SetCell(ICheckShipSlot slotChecker, IShipAssign assigner, bool isAttached);
}

namespace UserInterface
{
    public class ShipInventoryCell : InventoryListCell, IEquipmentCell
    {
        [Header("Cell Attributes")]
        public GameObject actionGroup;
        public Button informationButton;
        public Button attachButton;
        public Button detachButton;

        // Interfaces
        private IShipAssign assignerAction;
        private ICheckShipSlot slotChecker;

        private bool isActionsVisible = false;

        public void SetCell(ICheckShipSlot slotChecker, IShipAssign assigner, bool isAttached)
        {
            this.slotChecker = slotChecker;
            this.assignerAction = assigner;

            DecideActionButton(isAttached);
        }

        private void DecideActionButton(bool isAttached)
        {
            if (isAttached)
            {
                detachButton.gameObject.SetActive(true);
                attachButton.gameObject.SetActive(false);
            }
            else
            {
                detachButton.gameObject.SetActive(false);
                attachButton.gameObject.SetActive(true);
            }

        }

        public void RevealActionGroup()
        {
            isActionsVisible = !isActionsVisible;
            actionGroup.SetActive(isActionsVisible);

            if (!slotChecker.CheckSlotAvailability())
            {
                attachButton.enabled = false;
            }
            else
            {
                attachButton.enabled = true;
            }
        }


        public void InvokeAttachAction()
        {
            assignerAction.AssignItem(equipmentID);
        }

        public void InvokeRemovalAction()
        {
            assignerAction.RemoveItem(equipmentID);
        }

        public override void RevealInformation()
        {
            infoPanelInterface.SetInfoPanel(this.equipmentID);
        }
    }

}
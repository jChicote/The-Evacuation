using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Evacuation.UserInterface
{
    public class ShipInventoryCell : InventoryListCell, IUICellChecker
    {
        [Header("Cell Attributes")]
        //public GameObject actionGroup;
        public Button informationButton;
        public Button attachButton;
        public Button detachButton;

        // Interfaces
        private IShipAssign assignerAction;
        private ICheckShipSlot slotChecker;

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

        public override void RevealActionGroup()
        {
            base.RevealActionGroup();

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
            assignerAction.AssignItem(instanceID);
        }

        public void InvokeRemovalAction()
        {
            assignerAction.RemoveItem(instanceID);
        }

        public override void RevealInformation()
        {
            informationPanel.SetInfoPanel(this.instanceID);
        }
    }

}
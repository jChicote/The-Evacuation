using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipInventoryCell : InventoryListCell
{
    [Header("Cell Attributes")]
    public GameObject actionGroup;
    public Button informationButton;
    public Button attachButton;

    // Interfaces
    private IShipAssign assignerAction;
    private ICheckShipSlot slotChecker;

    private bool isActionsVisible = false;

    public void RevealActionGroup()
    {
        isActionsVisible = !isActionsVisible;
        actionGroup.SetActive(isActionsVisible);

        if (!slotChecker.CheckSlotAvailability())
        {
            attachButton.enabled = false;
        } else
        {
            attachButton.enabled = true;
        }
    }

    public void InvokeAttachAction()
    {
        assignerAction.AssignItem(equipmentID);
    }
}

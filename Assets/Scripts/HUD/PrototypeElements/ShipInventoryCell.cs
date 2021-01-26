using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IEquipmentCell
{
    void SetCell(ICheckShipSlot slotChecker);
}

public class ShipInventoryCell : InventoryListCell, IEquipmentCell
{
    [Header("Cell Attributes")]
    public GameObject actionGroup;
    public Button informationButton;
    public Button attachButton;

    // Interfaces
    private IShipAssign assignerAction;
    private ICheckShipSlot slotChecker;

    private bool isActionsVisible = false;

    public void SetCell(ICheckShipSlot slotChecker)
    {
        this.slotChecker = slotChecker;
    }

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

    public override void RevealInformation()
    {
        infoPanelInterface.SetInfoPanel(this.equipmentID);
    }
}

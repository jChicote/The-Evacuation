using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public interface IInventoryCell
{
    void SetData(string stringID, string title, Sprite cellThumbnail, string cellPrice);
}

public class InventoryListCell : MonoBehaviour
{
    [Header("Cell Attributes")]
    public TextMeshProUGUI cellTitle;
    public Image cellThumbnail;
    public TextMeshProUGUI cellPrice;

    [Header("Cell Attributes")]
    public GameObject actionGroup;
    public Button informationButton;
    public Button attachButton;

    private string stringID;
    private IShipAssign assignerAction;

    public void SetData(string stringID, string title, Sprite cellThumbnail, string cellPrice)
    {
        this.stringID = stringID;
        this.cellTitle.text = title;
        this.cellThumbnail.sprite = cellThumbnail;
        this.cellPrice.text = cellPrice;
    }

    public void RevealActionGroup()
    {
        actionGroup.SetActive(true);
    }

    public void InvokeAssignAction()
    {
        assignerAction.AssignItem(stringID);
    }
}

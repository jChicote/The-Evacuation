using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public interface IInventoryCell
{
    void SetData(string stringID, string title, Sprite cellThumbnail, string cellPrice, EquipmentType type, IInfoPanel panel);
    void SetColor();
}

public abstract class InventoryListCell : MonoBehaviour, IInventoryCell
{
    [Header("Cell Attributes")]
    public TextMeshProUGUI cellTitle;
    public Image cellThumbnail;
    public Image cellBackgroundImage;
    public TextMeshProUGUI cellPrice;

    // Information view for item listing
    protected GameObject informationView;
    protected EquipmentType equipmentType;
    protected IInfoPanel infoPanelInterface;

    protected string equipmentID;

    public void SetData(string stringID, string title, Sprite cellThumbnail, string cellPrice, EquipmentType type, IInfoPanel panel)
    {
        this.equipmentID = stringID;
        this.cellTitle.text = title;
        this.cellThumbnail.sprite = cellThumbnail;
        this.cellPrice.text = cellPrice;
        this.equipmentType = type;
        this.infoPanelInterface = panel;
    }

    public virtual void RevealInformation()
    {
        infoPanelInterface.SetInfoPanel(equipmentID);
    }

    public void SetColor()
    {
        WeaponType type = SessionData.instance.GetWeaponItem(equipmentID).weaponType;
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
    }
}

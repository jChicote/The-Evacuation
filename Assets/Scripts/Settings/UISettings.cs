using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/UI Settings")]
public class UISettings : ScriptableObject
{
    [Header("UI Menus")]
    public GameObject mainMenuPrefab;

    [Header("UI HUD")]
    public GameObject mobileUIHUDPrefab;

    [Header("Prototype Elements")]
    public GameObject prototypeShopCell;
    public GameObject prototypeShipCell;
    public GameObject prototypeEquipmentCell;
    public GameObject prototypeInventoryCell;

    [Space]
    public GameObject emptyListCell;

    [Header("Color Configurations")]
    public Color redColor;
    public Color blueColor;
    public Color greenColor;
    public Color greyColor;

    public Color GetSpecifiedColor(CellColor color)
    {
        switch (color)
        {
            case CellColor.Red:
                return redColor;
            case CellColor.Green:
                return greenColor;
            case CellColor.Blue:
                return blueColor;
        }

        return greenColor;
    }
}

public enum CellColor
{
    Red,
    Green,
    Blue
} 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.UserInterface.LocationMarker;

[CreateAssetMenu(menuName = "Settings/UI Settings")]
public class UISettings : ScriptableObject
{
    [Header("UI Menus")]
    public GameObject mainMenuPrefab;
    public GameObject selectionMenuPrefab;

    [Header("UI HUD")]
    public GameObject mobileUIHUDPrefab;

    [Header("Prototype Elements")]
    public GameObject prototypeShopCell;
    public GameObject prototypeShipCell;
    public GameObject prototypeEquipmentCell;
    public GameObject prototypeInventoryCell;

    [Space]
    public GameObject prototypeSelectionCell;

    [Space]
    public GameObject emptyListCell;
    public GameObject emptyShipCell;

    [Header("Color Configurations")]
    public Color redColor;
    public Color blueColor;
    public Color greenColor;
    public Color greyColor;

    [Header("UI Shaders")]
    public Material greyscaleMat;

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

    [Header("Locator Position Marker Types")]
    public GameObject markerPrefab;
    public MarkerSprites[] markerTypes;
}

[System.Serializable]
public struct MarkerSprites
{
    public MarkerType type;
    public Sprite sprite;
}

public enum CellColor
{
    Red,
    Green,
    Blue
} 

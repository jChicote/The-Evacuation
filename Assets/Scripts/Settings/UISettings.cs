using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/UI Settings")]
public class UISettings : ScriptableObject
{
    [Header("UI Menus")]
    public GameObject mainMenuPrefab;
}

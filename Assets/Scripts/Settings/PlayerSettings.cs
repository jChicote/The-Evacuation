using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Player Setting")]
public class PlayerSettings : ScriptableObject
{
    public Shipinfo[] shipsList;
}

[System.Serializable]
public struct Shipinfo
{
    public string shipName;
    public string stringID;

    [Header("Data")]
    public ShipStats stats;

    [Header("Prefab")]
    public GameObject shipPrefab;
}

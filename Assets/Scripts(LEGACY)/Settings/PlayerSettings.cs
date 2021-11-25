using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Player Setting")]
public class PlayerSettings : ScriptableObject
{
    public GameObject playerSpawnManager;
    public ShipAsset[] shipsList;

    /// <summary>
    /// Retrieves the asset from the string identification.
    /// </summary>
    public ShipAsset SearchThroughList(string stringID)
    {
        foreach (ShipAsset asset in shipsList)
        {
            if (asset.instanceID == stringID)
            {
                return asset;
            }
        }

        return null;
    }
}


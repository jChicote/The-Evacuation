using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Level Setting")]
public class LevelSettings : ScriptableObject
{
    [Header("Default Setting")]
    public LevelData[] defaultLevelData;

    // Tracker
    [Space]
    public GameObject sceneActorTrackerPrefab;
}

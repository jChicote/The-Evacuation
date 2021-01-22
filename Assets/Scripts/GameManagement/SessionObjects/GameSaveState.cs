using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveState
{
    public HangarInventory hangarSave;

    public HangarInventory GetHangarSave()
    {
        return hangarSave;
    }

    public void SaveHangar(HangarInventory hangar)
    {
        this.hangarSave = hangar;
    }

}

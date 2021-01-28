using System.Linq;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SessionData : MonoBehaviour
{
    // the session data will be the main model class that interfaces with the files.
    // Should be responsible for saving and extracting data.

    public static SessionData instance = null;

    public HangarInventory hangarCurrentSave;
    public UserStatus userStatus;

    // Contains variables that relate to the active variables used for the game
    [Header("Session State")]
    public ShipInfo selectedShip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Load();
    }

    /// <summary>
    /// Saves current state of game into device storage.
    /// </summary>
    public void Save()
    {
        // This can be called multiple times throughout the session.
        // 1. Create new game save
        GameSaveState newGameSave = new GameSaveState();

        // 2. Write saved instances
        newGameSave.SaveHangar(hangarCurrentSave);

        // 3. Serialise file and Save
        string jsonData = JsonUtility.ToJson(newGameSave, false);
        string jsonPath = Application.persistentDataPath + "/gamesave.save";

        File.WriteAllText(jsonPath, jsonData);
        Debug.Log("Finihsed Writing");
    }

    /// <summary>
    /// Loads the stored game state to session.
    /// </summary>
    public void Load()
    {
        // Designed to be only called once without recalls to file data.
        // Incorrect calls could overwrite data.
        string jsonPath = Application.persistentDataPath + "/gamesave.save";

        if (File.Exists(jsonPath))
        {
            // 1. Fetch from path
            GameSaveState saveState = JsonUtility.FromJson<GameSaveState>(File.ReadAllText(jsonPath));
            Debug.Log("State has been loaded");

            // 2. Read saved instances
            hangarCurrentSave = saveState.GetHangarSave();

            if (hangarCurrentSave == null)
            {
                SetupDefaultPlayer();
            }
        }
        else
        {
            SetupDefaultPlayer();
        }
    }

    /// <summary>
    /// Loads default items and ship when state returns null
    /// </summary>
    private void SetupDefaultPlayer()
    {
        Debug.LogWarning("No game save state or data was found. Creating a new default state");

        HangarInventory hangarSaveState = new HangarInventory();

        // 2. Load default items
        List<WeaponInfo> weaponList = new List<WeaponInfo>();
        weaponList.Add(GameManager.Instance.weaponSettings.turrentWeapons[0].ConvertToWeaponInfo());

        List<ShipInfo> shipList = new List<ShipInfo>();
        shipList.Add(GameManager.Instance.playerSettings.shipsList[0].ConvertToShipInfo());

        hangarSaveState.UpdateHangarWeapons(weaponList);
        hangarSaveState.UpdateHangarShips(shipList);

        // 3. Save to global
        hangarCurrentSave = hangarSaveState;
    }

    /// <summary>
    /// Resets current save state and loads defaults.
    /// </summary>
    public void ResetAllSaves()
    {
        GameSaveState newGameSave = new GameSaveState();
        string jsonData = JsonUtility.ToJson(newGameSave, false);
        string jsonPath = Application.persistentDataPath + "/gamesave.save";

        File.WriteAllText(jsonPath, jsonData);
        Debug.Log("Finihsed Reset");

        SetupDefaultPlayer();
    }

    public WeaponInfo GetWeaponItem(string weaponID)
    {
        // To limit the clunckiness of passing weapon info around, each vessel will instead store only the string reference.

        return hangarCurrentSave.hangarWeapons.Where(x => x.stringID == weaponID).First();
    }

    public int GetWeaponInstanceCount(string name)
    {
        int count =  hangarCurrentSave.hangarWeapons.Where(x => x.name == name).Count();
        return count;
    }

    public void RemoveWeaponInstance(string name)
    {
        WeaponInfo removedObject = hangarCurrentSave.hangarWeapons.Where(x => x.name == name && x.isAttached == false).First();
        hangarCurrentSave.hangarWeapons.Remove(removedObject);
    }

    public void AddWeaponInstance(WeaponAsset asset)
    {
        WeaponInfo info = asset.ConvertToWeaponInfo();
        hangarCurrentSave.hangarWeapons.Add(info);
    }

    public ShipInfo GetShipItem(string shipID) 
    {
        // As there are no duplicates storing ship identifiers through string, hangar will only store id for simplification.
        // Only will fetch and store ship info of vessels that are either in use or unlocked.

        return hangarCurrentSave.hangarShips.Where(x => x.stringID == shipID).First();
    }
}

public class UserStatus
{
    public int userLevel;
    public int credits;

    public int goalsCollected;
    public int totalKills;
    public int peopleSaved;

}
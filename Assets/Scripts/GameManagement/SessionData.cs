using System;
using System.Linq;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using Evacuation.Actor;

namespace Evacuation.Session
{
    public class SessionData : MonoBehaviour
    {
        // the session data will be the main model class that interfaces with the files.
        // Should be responsible for saving and extracting data.

        public static SessionData instance = null;

        public UserStatus userStatus;

        [Header("Data Events")]
        public UnityEvent OnUserTransaction;

        // Contains variables that relate to the active variables used for the game
        [Header("Session State")]
        public ShipInfo selectedShip;

        //Data Handlers (The game's 'model' handling interfacing with the data)
        public WeaponDataServicer weaponServicer;
        public ShipDataServicer shipServicer;

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

            weaponServicer = new WeaponDataServicer();
            shipServicer = new ShipDataServicer();
        }

        /// <summary>
        /// Saves current state of game into device storage.
        /// </summary>
        public void Save()
        {
            // This can be called multiple times throughout the session.
            // 1. Create new game save
            GameSaveState newGameSave = new GameSaveState();
            HangarInventory hangarInventory = new HangarInventory();

            hangarInventory.hangarShips = shipServicer.GetHangarShips();
            hangarInventory.hangarWeapons = weaponServicer.GetHangarWeapons();

            // 2. Write saved instances
            newGameSave.SaveHangar(hangarInventory);
            newGameSave.SaveUserStatus(userStatus);

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
                //Debug.Log("State has been loaded");

                // 2. Read saved instances
                HangarInventory hangarCurrentSave = saveState.GetHangarSave();
                shipServicer.SetHangarShips(hangarCurrentSave.hangarShips);
                weaponServicer.SetHangarShips(hangarCurrentSave.hangarWeapons);
                userStatus = saveState.GetUserStatus();

                if (hangarCurrentSave == null || saveState.userStatus == null)
                {
                    Debug.LogWarning("Detecting missing or data loss");
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

            //HangarInventory hangarSaveState = new HangarInventory();

            // 2. Load default items
            List<WeaponInfo> weaponList = new List<WeaponInfo>();
            weaponList.Add(GameManager.Instance.weaponSettings.turrentWeapons[0].ConvertToWeaponInfo());

            //hangarSaveState.UpdateHangarWeapons(weaponList);
            weaponServicer.SetHangarShips(weaponList);
            shipServicer.ResetAllShips();

            // 3. Save to global
            //hangarCurrentSave = hangarSaveState;

            UserStatus newuser = new UserStatus();
            userStatus = newuser;
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
    }

    [System.Serializable]
    public class UserStatus
    {
        public int userLevel = 0;
        public int credits = 4000;

        public int goalsCollected = 0;
        public int totalKills = 0;
        public int peopleSaved = 0;

    }
}
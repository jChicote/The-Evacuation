using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Session
{
    [System.Serializable]
    public class GameSaveState
    {
        public HangarInventory hangarSave;
        public UserStatus userStatus;

        public HangarInventory GetHangarSave()
        {
            return hangarSave;
        }

        public UserStatus GetUserStatus()
        {
            return userStatus;
        }

        public void SaveHangar(HangarInventory hangar)
        {
            this.hangarSave = hangar;
        }

        public void SaveUserStatus(UserStatus userStatus)
        {
            this.userStatus = userStatus;
        }
    }
}

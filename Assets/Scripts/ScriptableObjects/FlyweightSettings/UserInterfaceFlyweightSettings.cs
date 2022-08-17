using System;
using TheEvacuation.Infrastructure.Score.UpdateScoreRecord;
using TheEvacuation.Model.Entities;
using UnityEngine;

namespace TheEvacuation.ScriptableObjects.FlyweightSettings
{

    [CreateAssetMenu(menuName = "Settings/User Interface Flyweight Settings")]
    public class UserInterfaceFlyweightSettings : ScriptableObject
    {

        #region - - - - - - Fields - - - - - -

        public AvatarImage[] avatarImages;

        [Space]
        public GameObject playerSelectionCellPrefab;
        public GameObject shipSelectionCellPrefab;
        public GameObject loadingScreenPrefab;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public int GetIdentifierFromSearchImage(Sprite sprite)
        {
            int identifier = 0;

            for (int i = 0; i < avatarImages.Length; i++)
            {
                if (avatarImages[i].avatarSprite == sprite)
                    return i;
            }

            return identifier;
        }

        public GameObject GetSelectedScorePopupPrefab(ScoreEventType eventType)
        {
            throw new NotImplementedException();
        }

        #endregion Methods

    }

}

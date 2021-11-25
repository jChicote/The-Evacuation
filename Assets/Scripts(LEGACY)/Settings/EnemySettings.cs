using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Actor.EnemySystems;

namespace Evacuation.Settings
{
    [CreateAssetMenu(menuName = "Settings/Enemy Setting")]
    public class EnemySettings : ScriptableObject
    {
        public GameObject droidSentryPrefab;

        public EnemyAsset[] enemyList;

        /// <summary>
        /// Retrieves the asset from the string identification.
        /// </summary>
        public EnemyAsset SearchThroughList(string stringID)
        {
            foreach (EnemyAsset asset in enemyList)
            {
                if (asset.instanceID == stringID) return asset;
            }

            return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Evacuation.Actor.EnemySystems
{
    [Serializable]
    public class EnemyAsset : ObjectInfo
    {
        public EnemyShipStats enemyStats;
        public GameObject enemyPrefab;

        public EnemyInfo ConvertToEnemyInfo()
        {
            EnemyInfo info = new EnemyInfo();

            info.SetData(
                stringID,
                name,
                enemyStats.maxHealth,
                enemyStats.maxSheild,
                enemyStats.maxSpeed,
                enemyStats.maxHandling,
                enemyStats.weaponSize);

            return info;
        }
    }
}

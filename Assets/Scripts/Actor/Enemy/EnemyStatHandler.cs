using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Actor.EnemySystems
{
    public class EnemyStatHandler : BaseStatHandler
    {
        public EnemyInfo enemyInfo;

        public void InitialiseStats(EnemyInfo enemyInfo)
        {
            this.enemyInfo = enemyInfo;
        }

        public List<string> GetForwardLoadout()
        {
            return enemyInfo.FixedWeapons;
        }
    }
}

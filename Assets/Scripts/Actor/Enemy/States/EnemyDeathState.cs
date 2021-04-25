using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Evacuation.Actor.EnemySystems
{
    public class EnemyDeathState : BaseComponentState
    {
        public override void BeginState()
        {
            print("Enemy has been destroyed");
            Destroy(gameObject);
        }
    }
}

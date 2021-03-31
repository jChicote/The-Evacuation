using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Actor.EnemySystems
{
    public class DroidSentryEnemyController : BaseEnemyController
    {
        public override void InitialiseController()
        {
            InitialiseMovementSystems();
            InitialiseWeaponSystems();
        }

        private void InitialiseMovementSystems()
        {
            EnemyStateManager stateManager = this.GetComponent<EnemyStateManager>();
            stateManager.AddState<EnemyFollowState>();
            //EnemyMovementController movementController = this.GetComponent<EnemyMovementController>();
        }

        private void InitialiseWeaponSystems()
        {
            //EnemyWeaponController weaponController = this.GetComponent<EnemyWeaponController>();
        }
    }
}

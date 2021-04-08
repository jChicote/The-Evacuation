using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Evacuation.Level.SpawnManagement;

namespace Evacuation.Actor.EnemySystems
{
    public class DroidSentryEnemyController : BaseEnemyController
    {
        // Inspector accessible fields
        [SerializeField] private string referenceID;

        // Fields
        private EnemyInfo enemyInfo;
        private EnemyStateManager stateManager;

        public override void InitialiseController()
        {
            InitialiseStats();
            InitialiseVitality();
            InitialiseMovementSystems();
            InitialiseWeaponSystems();
        }

        private void InitialiseStats()
        {
            enemyInfo = GameManager.Instance.enemySettings.enemyList.Where(x => x.stringID == referenceID).First().ConvertToEnemyInfo();

            EnemyStatHandler statHandler = this.GetComponent<EnemyStatHandler>();
            statHandler.InitialiseStats(enemyInfo);

        }

        private void InitialiseMovementSystems()
        {                                           
            EnemyStateManager stateManager = this.GetComponent<EnemyStateManager>();
            //stateManager.AddState<EnemyFollowState>();
            //EnemyMovementController movementController = this.GetComponent<EnemyMovementController>();
        }

        private void InitialiseWeaponSystems()
        {
            //EnemyWeaponController weaponController = this.GetComponent<EnemyWeaponController>();
            EnemyDamageManager damageManager = this.GetComponent<EnemyDamageManager>();
            damageManager.InitialiseComponent();
        }

        private void InitialiseVitality()
        {
            EnemyHealthComponent healthComponent = this.GetComponent<EnemyHealthComponent>();
            healthComponent.InitialiseHealth(enemyInfo.maxHealth);
        }

        public override void SetEntryState(SpawnPattern pattern)
        {
            switch(pattern)
            {
                case SpawnPattern.FollowIn:
                    stateManager.AddState<EnemyFollowState>();
                    break;
                default:
                    stateManager.AddState<EnemyFollowState>();
                    break;
            }
        }
    }
}

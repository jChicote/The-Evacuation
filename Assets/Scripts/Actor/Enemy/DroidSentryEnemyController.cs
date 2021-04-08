using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Evacuation.Actor.EnemySystems
{
    public class DroidSentryEnemyController : BaseEnemyController
    {
        [SerializeField] private string referenceID;
        private EnemyInfo enemyInfo;

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
            stateManager.AddState<EnemyFollowState>();
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
    }
}

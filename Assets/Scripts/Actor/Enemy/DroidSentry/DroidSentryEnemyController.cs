using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Evacuation.Level.SpawnManagement;
using Evacuation.Model.Data;

namespace Evacuation.Actor.EnemySystems.DroidSystems
{
    public class DroidSentryEnemyController : BaseEnemyController
    {
        // Inspector accessible fields
        [SerializeField] private string referenceID;

        // Fields
        private ShipData shipData;
        private IStateManager stateManager;

        public override void InitialiseController()
        {
            InitialiseStats();
            InitialiseVitality();
            InitialiseMovementSystems();
            InitialiseWeaponSystems();
        }

        private void InitialiseStats()
        {
            EnemyInfo enemyInfo = GameManager.Instance.enemySettings.enemyList.Where(x => x.stringID == referenceID).First().ConvertToEnemyInfo();
            shipData = enemyInfo.GetShipData();

            EnemyStatHandler statHandler = this.GetComponent<EnemyStatHandler>();
            statHandler.InitialiseStats(enemyInfo);
        }

        private void InitialiseMovementSystems()
        {                                           
            IStateManager stateManager = this.GetComponent<IStateManager>();
            stateManager.AddState<EnemyFollowState>();
            IMovementController movementController = this.GetComponent<IMovementController>();
            movementController.InitialiseController();
        }

        private void InitialiseWeaponSystems()
        {
            IEnemyTargetingSystem targetingSystem = this.GetComponent<IEnemyTargetingSystem>();
            targetingSystem.InitialiseTargetingSystem();

            IWeaponController weaponController = this.GetComponent<IWeaponController>();
            weaponController.InitialiseWeaponController();

            IDamageable damageManager = this.GetComponent<IDamageable>();
            damageManager.InitialiseComponent();
        }

        private void InitialiseVitality()
        {
            IHealthComponent healthComponent = this.GetComponent<IHealthComponent>();
            healthComponent.InitialiseHealth(shipData.MaxHealth);
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

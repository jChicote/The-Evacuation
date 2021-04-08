using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Actor.EnemySystems;

namespace Evacuation.Level.SpawnManagement
{
    public class DroidSpawnManager : SpawnManager
    {
        // Fields
        protected GameManager gameManager;
        protected SceneController sceneController;
        protected SpawnPositioner spawnPositioner;

        private void Start()
        {
            InitialiseManager();
        }

        public override void InitialiseManager()
        {
            base.InitialiseManager();
            gameManager = GameManager.Instance;
            sceneController = gameManager.sceneController;
        }

        public override void SpawnEntity()
        {
            base.SpawnEntity();
            /*GameObject spawnedDroid = Instantiate(gameManager.enemySettings.droidSentryPrefab, transform.position, Quaternion.identity);

            IAssignSceneActorTracker assignTracker = spawnedDroid.GetComponent<IAssignSceneActorTracker>();
            assignTracker.SetSceneActorTracker(sceneController.ActorTracker);
            sceneController.ActorTracker.RegisterEnemyEntity(spawnedDroid);

            IEnemyController enemyController = spawnedDroid.GetComponent<IEnemyController>();
            enemyController.InitialiseController();*/

            //spawnPositioner.PositionEntity(spawnedDroid, enemyController);
        }

        public override void GloballyClearAllEntities()
        {
            base.GloballyClearAllEntities();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Actor.EnemySystems;

namespace Evacuation.Level.SpawnManagement
{
    public class DroidSpawnManager : SpawnManager
    {
        // Inspector Accessible Fields
        [SerializeField] protected float maximumEntityCount;

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
            spawnPositioner = this.GetComponent<SpawnPositioner>();
        }

        public override void SpawnEntity()
        {
            if (entityCount > maximumEntityCount) return;

            base.SpawnEntity();

            GameObject spawnedDroid = Instantiate(gameManager.enemySettings.droidSentryPrefab, transform.position, Quaternion.identity);
            IStatePatternSetter patternSetter = spawnedDroid.GetComponent<IStatePatternSetter>();

            InitialiseEntity(spawnedDroid);
            spawnPositioner.PositionEntity(spawnedDroid, patternSetter);
            spawnedDroid.SetActive(true);

            entityCount++;
        }

        public void InitialiseEntity(GameObject entity)
        {
            IAssignSceneActorTracker assignTracker = entity.GetComponent<IAssignSceneActorTracker>();
            assignTracker.SetSceneActorTracker(sceneController.ActorTracker);
            sceneController.ActorTracker.RegisterEnemyEntity(entity);

            IEnemyController enemyController = entity.GetComponent<IEnemyController>();
            enemyController.InitialiseController();
            entity.SetActive(false);
        }

        public override void GloballyClearAllEntities()
        {
            base.GloballyClearAllEntities();
        }
    }
}

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
        protected ISpawnPositioner spawnPositioner;

        private void Start()
        {
            InitialiseManager();
        }

        public override void InitialiseManager()
        {
            base.InitialiseManager();
            gameManager = GameManager.Instance;
            sceneController = gameManager.sceneController;
            spawnPositioner = this.GetComponent<ISpawnPositioner>();
        }

        public override void SpawnEntity()
        {
            base.SpawnEntity();
            if (entityCount > maximumEntityCount) return;

            GameObject spawnedDroid = Instantiate(gameManager.enemySettings.droidSentryPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            IStatePatternSetter patternSetter = spawnedDroid.GetComponent<IStatePatternSetter>();

            print(entityCount);
            entityCount++;

            InitialiseEntity(spawnedDroid);
            spawnPositioner.PositionEntity(spawnedDroid, patternSetter);
            spawnedDroid.SetActive(true);
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

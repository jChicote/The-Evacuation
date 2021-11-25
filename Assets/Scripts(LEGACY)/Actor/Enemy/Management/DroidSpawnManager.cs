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
        private IStatePatternSetter[] allEntityPatternSetters = new IStatePatternSetter[0];

        public override void InitialiseSpawner()
        {
            base.InitialiseSpawner();
            gameManager = GameManager.Instance;
            sceneController = gameManager.sceneController;
            spawnPositioner = this.GetComponent<ISpawnPositioner>();

            sceneController.OnPlatformLandingEvent.AddListener(OnPlayerLanding);
        }

        public override void SpawnEntity()
        {
            base.SpawnEntity();
            if (entityCount > maximumEntityCount) return;
            entityCount++;

            GameObject spawnedDroid = Instantiate(gameManager.enemySettings.droidSentryPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            IStatePatternSetter patternSetter = spawnedDroid.GetComponent<IStatePatternSetter>();
            AddNewEntityPatternSetter(patternSetter);

            spawnPositioner.PositionEntity(spawnedDroid, patternSetter);
            spawnedDroid.SetActive(true);
        }

        private void AddNewEntityPatternSetter(IStatePatternSetter newSetter)
        {
            print(newSetter);
            IStatePatternSetter[] tempArray = new IStatePatternSetter[allEntityPatternSetters.Length + 1];

            for (int i = 0; i < allEntityPatternSetters.Length; i++)
            {
                tempArray[i] = allEntityPatternSetters[i];
            }

            tempArray[tempArray.Length - 1] = newSetter;
            allEntityPatternSetters = tempArray;
        }

        public override void GloballyClearAllEntities()
        {
            base.GloballyClearAllEntities();
        }

        public void OnPlayerLanding(bool hasLanded)
        {
            if (allEntityPatternSetters == null || allEntityPatternSetters.Length == 0) return;

            foreach (IStatePatternSetter patternSetter in allEntityPatternSetters)
            {
                //(patternSetter);
                patternSetter.OnPlayerHasLanded(hasLanded);
            }
        }
    }
}

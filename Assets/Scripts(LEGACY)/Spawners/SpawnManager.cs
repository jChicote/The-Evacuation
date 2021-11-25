using UnityEngine;

public interface IActionSwitch
{

}

namespace Evacuation.Level.SpawnManagement
{
    public interface ISpawnCountTracker
    {
        void DeductEntityCount();
    }

    public class SpawnManager : MonoBehaviour, IPausable, IActionSwitch, ISpawnCountTracker, ISpawnManager
    {
        // Inspector Accessible Fields
        [SerializeField] protected GameObject entityPrefab;
        [SerializeField] protected float spawnIntervalTime;

        // Fields
        protected SimpleTimer timer;
        protected int entityCount;
        protected bool isPaused = false;

        public virtual void InitialiseSpawner() 
        {
            timer = new SimpleTimer(spawnIntervalTime, Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (isPaused) return;
            timer.TickTimer();
            SpawnEntity();
        }

        public virtual void SpawnEntity() 
        {
            if (!timer.CheckTimeIsUp()) return;
            timer.ResetTimer();
        }

        public virtual void GloballyClearAllEntities() { }

        public void DeductEntityCount()
        {
            entityCount--;
        }

        public void OnPause()
        {
            isPaused = true;
        }

        public void OnUnpause()
        {
            isPaused = false;
        }
    }
}
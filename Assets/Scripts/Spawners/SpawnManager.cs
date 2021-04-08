using UnityEngine;

public interface IActionSwitch
{

}

namespace Evacuation.Level.SpawnManagement
{
    public class SpawnManager : MonoBehaviour, IPausable, IActionSwitch
    {
        // Inspector Accessible Fields
        [SerializeField] protected GameObject entityPrefab;
        [SerializeField] protected float spawnIntervalTime;

        // Fields
        protected SimpleTimer timer;
        protected bool isPaused = false;

        public virtual void InitialiseManager() 
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
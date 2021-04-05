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

        // Fields
        protected SimpleTimer timer;
        protected bool isPaused = false;

        public virtual void InitialiseManager() 
        {

        }

        private void FixedUpdate()
        {
            if (isPaused) return;
            SpawnEntity();
        }

        public virtual void SpawnEntity() 
        {
            if (timer.CheckTimeIsUp())
            {
                // Create Object
                timer.ResetTimer();
            }
        }

        public virtual void GloballyClearAllEnemies() { }

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

public class SimpleTimer
{
    private float intervalLength;
    private float timeLeft;
    private float deltaTime;

    public SimpleTimer(float intervalLength, float deltaTime)
    {
        this.intervalLength = intervalLength;
        this.timeLeft = intervalLength;
        this.deltaTime = deltaTime;
    }

    public void TickTimer()
    {
        timeLeft -= deltaTime;
    }

    public bool CheckTimeIsUp()
    {
        return timeLeft <= 0;
    }

    public void ResetTimer()
    {
        timeLeft = intervalLength;
    }
}


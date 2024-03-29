using System;
using TheEvacuation.Common;
using UnityEngine;
using UnityEngine.Events;

namespace TheEvacuation.TimeUtility
{

    public class GameTimer : MonoBehaviour, IPausable
    {

        #region - - - - - - Fields - - - - -

        public UnityEvent OnTimerStart;
        public TimerTickEvent OnTimerTick;
        public UnityEvent OnTimerEnd;

        [SerializeField]
        protected float timerDuration;
        protected float timeLeft;

        [Space]
        [SerializeField]
        protected bool beginsOnStart;
        protected bool isCountingDown = false;
        protected bool isPaused = false;

        public bool IsPaused { get => isPaused; set => isPaused = value; }

        #endregion Fields

        #region - - - - - - MonoBehaviour - - - - -

        private void Start()
        {
            if (beginsOnStart)
                BeginTimer();
        }

        private void Update()
        {
            if (!isCountingDown || isPaused) return;

            this.UpdateTime();
            this.OnTimerTick?.Invoke(this.timeLeft, timerDuration);

            if (timeLeft <= 0)
            {
                this.OnTimerEnd?.Invoke();
                this.ResetTimer();
            }
        }

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - -

        public void BeginTimer()
        {
            this.timeLeft = this.timerDuration;
            isCountingDown = true;
            OnTimerStart?.Invoke();
        }

        public void ResetTimer()
        {
            this.isCountingDown = false;
            this.timeLeft = timerDuration;

        }

        public void UpdateTime()
            => timeLeft -= Time.deltaTime;

        public void OnPauseEntity()
            => isPaused = true;

        public void OnUnpauseEntity()
            => isPaused = false;

        #endregion Methods

    }

    [Serializable]
    public class TimerTickEvent : UnityEvent<float, float> { }

}
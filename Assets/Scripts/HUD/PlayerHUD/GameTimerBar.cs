using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterfaces.HUD
{
    public interface IGameTimerBar
    {
        void InitialiseTimerBar(float maxTime);
    }

    public class GameTimerBar : VitalityBarComponent, IGameTimerBar
    {
        [SerializeField] private Timer gameTimer;

        public void InitialiseTimerBar(float maxTime)
        {
            InitialiseBar(maxTime);
            gameTimer.SetTimer(maxTime);
            gameTimer.StartTimer();
        }

        private void FixedUpdate()
        {
            SetBarValue(gameTimer.GetRemainingTime());
        }

        public void TriggerGameEndState()
        {
            Debug.LogWarning("Game Has Ended!!!!!!!!!!");
        }
    }
}

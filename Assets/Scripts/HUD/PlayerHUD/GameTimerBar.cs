using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.UserInterface.HUD
{
    public interface IGameTimerBar
    {
        void InitialiseTimerBar(SceneController sceneController, float maxTime);
    }

    public class GameTimerBar : VitalityBarComponent, IGameTimerBar
    {
        [SerializeField] private Timer gameTimer;
        private SceneController sceneController;

        public void InitialiseTimerBar(SceneController sceneController, float maxTime)
        {
            this.sceneController = sceneController;
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
            sceneController.OnGameCompletion.Invoke();
        }
    }
}

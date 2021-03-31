using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.UserInterface.HUD
{
    public interface IScoreTextUpdater
    {
        void SetScoreListener(IScoreEventAssigner eventAssigner);
        void UpdateScoreText(ScoreData scoreData);
    }

    public class ScoreLabel : LabelBox, IScoreTextUpdater
    {
        public override void InitialiseLUILabel()
        {
            labelText.text = "0";
        }

        /// <summary>
        /// Sets interface method to listen to score updates.
        /// </summary>
        public void SetScoreListener(IScoreEventAssigner eventAssigner)
        {
            // assigns method action to score event
            eventAssigner.GetScoreEvent().AddListener(UpdateScoreText);
        }

        /// <summary>
        /// Called to update the score text through triggerent unity events.
        /// </summary>
        public void UpdateScoreText(ScoreData scoreData)
        {
            Debug.Log(scoreData);
            labelText.text = scoreData.earnedScore.ToString();
        }
    }
}
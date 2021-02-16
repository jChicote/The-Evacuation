using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterfaces.HUD
{
    public class RescueLabel : LabelBox, IScoreTextUpdater
    {
        public override void InitialiseLUILabel()
        {
            labelText.text = "0/0";
        }

        /// <summary>
        /// Sets interface method to listen to score updates.
        /// </summary>
        public void SetScoreListener(IScoreEventAssigner eventAssigner)
        {
            eventAssigner.GetScoreEvent().AddListener(UpdateScoreText);
        }

        /// <summary>
        /// Called to update the score text through triggerent unity events.
        /// </summary>
        public void UpdateScoreText(ScoreData scoreData)
        {
            labelText.text = scoreData.totalRescued + "/" + scoreData.maxRescuable;
        }
    }
}

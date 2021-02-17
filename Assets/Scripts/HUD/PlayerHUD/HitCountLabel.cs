using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterfaces.HUD
{
    public class HitCountLabel : LabelBox, IScoreTextUpdater
    {
        public override void InitialiseLUILabel()
        {
            labelText.enabled = false;
        }

        public void SetScoreListener(IScoreEventAssigner eventAssigner)
        {
            eventAssigner.GetScoreEvent().AddListener(UpdateScoreText);
            labelText.enabled = false;
        }

        public void UpdateScoreText(ScoreData scoreData)
        {
            labelText.text = scoreData.hitCount.ToString();

            if(scoreData.hitCount == 0)
            {
                labelText.enabled = false;
            } 
            else
            {
                labelText.enabled = true;
            }
        }
    }
}

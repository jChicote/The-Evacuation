using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.UserInterface.HUD
{
    public class EarnedScoreCell : MissionReportCell
    {
        // Fields
        private int topScore;
        private int lerpScoreValue = 0;

        public override void InitialiseCell(LevelData levelData)
        {
            topScore = levelData.topScore;
        }

        private void FixedUpdate()
        {
            if (isUpdated) return;

            DisplayText();
        }

        public override void DisplayText()
        {
            lerpScoreValue = (int)Mathf.Lerp(lerpScoreValue, topScore, 0.5f);
            cellLabel.text = lerpScoreValue.ToString() + " pts";

            if (lerpScoreValue >= topScore)
            {
                isUpdated = true;
            }
        }
    }
}

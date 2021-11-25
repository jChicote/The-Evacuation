using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.UserInterface.HUD
{
    public class RescuedCell : MissionReportCell
    {
        // Fields
        private int rescueCount;
        private int maxRescuable;
        private int lerpRescueValue = 0;

        public override void InitialiseCell(LevelData levelData)
        {
            maxRescuable = levelData.maxRescuable;
            rescueCount = levelData.totalRescued;
        }

        private void FixedUpdate()
        {
            if (isUpdated) return;

            DisplayText();
        }

        public override void DisplayText()
        {
            lerpRescueValue = (int)Mathf.Lerp(lerpRescueValue, rescueCount, 0.5f);
            cellLabel.text = lerpRescueValue.ToString() + "/" + maxRescuable + " Rescued";

            if (lerpRescueValue >= maxRescuable)
            {
                isUpdated = true;
            }
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.UserInterface.HUD
{
    public class KillCountCell : MissionReportCell
    {
        private int killCount;
        private int lerpKillCount;

        public override void InitialiseCell(LevelData levelData)
        {
            killCount = levelData.totalKills;
        }

        private void FixedUpdate()
        {
            if (isUpdated) return;

            DisplayText();
        }

        public override void DisplayText()
        {
            lerpKillCount = (int)Mathf.Lerp(lerpKillCount, killCount, 0.5f);
            cellLabel.text = lerpKillCount.ToString() + "/" + killCount + " Rescued";

            if (lerpKillCount >= killCount)
            {
                isUpdated = true;
            }
        }
    }
}

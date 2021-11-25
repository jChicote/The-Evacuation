using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.UserInterface.HUD
{
    public class GameCompletionHUD : MonoBehaviour
    {
        [SerializeField] private MissionReportCell[] reportCells;

        public void InitialiseGameCompletionHUD(LevelData levelData)
        {
            foreach (MissionReportCell cell in reportCells)
            {
                cell.InitialiseCell(levelData);
            }
        }

        public void CloseLevel()
        {
            GameManager.Instance.sceneLoader.LoadMainMenu();
        }
    }
}
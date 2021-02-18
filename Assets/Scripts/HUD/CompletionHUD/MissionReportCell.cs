using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UserInterface.HUD
{
    public abstract class MissionReportCell : MonoBehaviour
    {
        // Fields
        [SerializeField] protected TextMeshProUGUI cellLabel;
        protected bool isUpdated = false;

        public abstract void InitialiseCell(LevelData levelData);

        public virtual void DisplayText() { }
    }
}

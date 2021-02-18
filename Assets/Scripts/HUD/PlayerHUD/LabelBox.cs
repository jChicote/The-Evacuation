using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UserInterface.HUD
{
    public abstract class LabelBox : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI labelText;

        public abstract void InitialiseLUILabel();
        public virtual void ResetLabel() { }
    }
}

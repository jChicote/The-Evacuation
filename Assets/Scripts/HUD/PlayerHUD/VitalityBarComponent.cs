using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Evacuation.UserInterface.HUD
{
    public interface IVitalityBar
    {
        void InitialiseBar(float maxValue);
        void SetBarValue(float amount);
    }

    public class VitalityBarComponent : MonoBehaviour, IVitalityBar
    {
        [SerializeField] private RectTransform barRect;
        [SerializeField] private Image barFill;

        private float maxValue;
        private float currentValue;

        public void InitialiseBar(float maxValue)
        {
            this.maxValue = maxValue;

            barRect = this.GetComponent<RectTransform>();
        }

        public void SetBarValue(float amount)
        {
            if ((amount / maxValue) < 0) return;

            currentValue = (amount / maxValue) * barRect.rect.width;
            // Calculates the length by modifying the rightmost offset of the bar's transform
            barFill.rectTransform.offsetMax = new Vector2(-(barRect.rect.width - currentValue), barFill.rectTransform.offsetMax.y);
        }
    }
}

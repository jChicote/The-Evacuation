using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterfaces.HUD
{
    public interface IVitalityBar
    {
        void InitialiseBar(float maxValue);
        void SetBarValue(float amount);
    }

    public class VitalityBarComponent : MonoBehaviour, IVitalityBar
    {
        public RectTransform barRect;
        public Image barFill;

        private float maxValue;
        private float currentValue;

        public void InitialiseBar(float maxValue)
        {
            this.maxValue = maxValue;

            barRect = this.GetComponent<RectTransform>();
        }

        public void SetBarValue(float amount)
        {
            currentValue = (amount / maxValue) * barRect.rect.width;
            barFill.rectTransform.right = Vector3.right * (barRect.rect.width - currentValue);
        }
    }
}

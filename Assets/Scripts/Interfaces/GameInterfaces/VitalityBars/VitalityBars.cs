using UnityEngine;
using UnityEngine.UI;

namespace TheEvacuation.Interfaces.GameInterfaces.VitalityBars
{

    public class VitalityBars : MonoBehaviour, IVitalityBars
    {

        #region - - - - - - Fields - - - - - -

        public Slider sliderBar;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public float GetBarValue()
            => sliderBar.value;

        public void SetBarValue(float value, float maximum)
            => sliderBar.value = value / maximum;

        #endregion Methods

    }

}

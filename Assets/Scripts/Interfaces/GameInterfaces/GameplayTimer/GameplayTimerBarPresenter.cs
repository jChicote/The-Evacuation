using UnityEngine;
using UnityEngine.UI;

namespace TheEvacuation.Interfaces.GameInterfaces.GameplayTimer
{

    public class GameplayTimerBarPresenter : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField]
        protected Slider slider;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void SetBarValue(float current, float originalValue)
            => slider.value = current / originalValue;

        #endregion Methods

    }

}

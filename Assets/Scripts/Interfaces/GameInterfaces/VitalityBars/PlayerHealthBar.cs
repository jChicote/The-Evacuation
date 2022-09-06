using UnityEngine;

namespace TheEvacuation.Interfaces.GameInterfaces.VitalityBars
{

    public class PlayerHealthBar : MonoBehaviour, IPlayerHealthBar
    {

        #region - - - - - - Fields - - - - - -

        private IVitalityBars vitalityBar;

        #endregion Fields

        #region - - - - - - MonoBehaviour - - - - - -

        private void Awake()
            => vitalityBar = this.GetComponent<IVitalityBars>();

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - - -

        public void SetPlayerHealthBarValue(float health, float maxHealth)
            => vitalityBar.SetBarValue(health, maxHealth);

        #endregion Methods

    }

}
using TheEvacuation.Interfaces.GameInterfaces.VitalityBars;
using UnityEngine;

namespace TheEvacuation.Tests.GameMode.UserInterfaces.GameInterfaces.Vitality
{

    public class Tests_VitalityBars : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public VitalityBars vitalityBars;

        public float maximumHelath = 100f;
        public float currentHealth = 100f;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        private void Start()
        {
            vitalityBars.SetBarValue(currentHealth, maximumHelath);
        }

        public void SetBarValue_TestValueDecrementingByTen_BarValueDecrementsUntilZero()
        {
            // Arrange

            // Act
            if (currentHealth < 0) return;

            vitalityBars.SetBarValue(currentHealth, maximumHelath);
            currentHealth -= 10f;

            // Assertion done in scene
            Debug.Log("Current Health: " + currentHealth + ", With Bar Set At: " + vitalityBars.GetBarValue());
        }

        #endregion Methods

    }

}

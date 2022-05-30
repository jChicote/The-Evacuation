using System.Collections;
using TheEvacuation.Interfaces.GameInterfaces.Text;
using UnityEngine;

namespace TheEvacuation.Interfaces.MenuInterfaces.ScoreBoard
{

    public class IncrementingLabel : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public DynamicText dynamicText;

        public float displayTime;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void DisplayScoreNumberOverTime(int value)
            => StartCoroutine(IterateAndRenderIncrementingValue(value));

        public IEnumerator IterateAndRenderIncrementingValue(int value)
        {
            float incrementStep = value / (displayTime / Time.deltaTime);
            int currentValue = 0;

            while (currentValue < value)
            {
                currentValue += (int)incrementStep;
                dynamicText.SetTextValue(Mathf.Clamp(currentValue, 0, value).ToString());
                yield return new WaitForSeconds(Time.deltaTime);
            }

            yield return null;
        }

        #endregion Methods

    }

}

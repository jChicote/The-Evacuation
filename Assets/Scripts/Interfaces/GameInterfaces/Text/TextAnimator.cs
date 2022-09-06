using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace TheEvacuation.Interfaces.GameInterfaces.Text
{

    public class TextAnimator : MonoBehaviour, ITextAnimator
    {

        #region - - - - - - Fields - - - - - -

        public IDynamicTextModifier dynamicTextModifier;

        [Range(0f, 0.5f)]
        public float animationSpeed = 0.05f;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void InitialiseTextAnimator(IDynamicTextModifier dynamicTextModifier)
        {
            this.dynamicTextModifier = dynamicTextModifier;
        }

        public IEnumerator AnimateThroughText(string input, UnityAction action)
        {
            char[] inputArray = input.ToCharArray();
            int index = 0;
            string outputString = "";

            while (input.Length != outputString.Length)
            {
                outputString += inputArray[index];
                dynamicTextModifier.SetTextValue(outputString);
                index++;

                yield return new WaitForSeconds(animationSpeed);
            }

            action?.Invoke();
        }

        #endregion Methods

    }

}



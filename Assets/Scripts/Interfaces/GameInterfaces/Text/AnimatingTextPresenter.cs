using UnityEngine;
using UnityEngine.Events;

namespace TheEvacuation.Interfaces.GameInterfaces.Text
{

    public class AnimatingTextPresenter : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        private IDynamicTextModifier dynamicTextModifier;
        private ITextAnimator textAnimator;

        #endregion Fields

        #region - - - - - - MonoBehaviour - - - - - -

        private void Start()
        {
            dynamicTextModifier = this.GetComponent<IDynamicTextModifier>();
            textAnimator = this.GetComponent<ITextAnimator>();

            textAnimator.InitialiseTextAnimator(dynamicTextModifier);
        }

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - - -

        public void DisplayAnimatingText(string textInput, UnityAction action)
        {
            if (dynamicTextModifier == null || textAnimator == null)
            {
                Debug.LogError("Missing text modifier and text animator.");
                return;
            }

            StartCoroutine(textAnimator.AnimateThroughText(textInput, action));
        }

        #endregion Methods

    }

}
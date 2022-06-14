using System.Collections;
using UnityEngine;

namespace TheEvacuation.Interfaces.GameInterfaces.FlexPanel
{

    public class FlexPanelTweenAnimator : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public FlexPanelPresenter panelPresenter;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void InitialiseFlexPanelTweenAnimator(FlexPanelPresenter panelPresenter)
        {
            this.panelPresenter = panelPresenter;
        }

        public IEnumerator TweenToTargetDimensions(float startingHeight, float targetHeight, float statingWidth, float targetWidth, float duration)
        {
            float currentHeight = startingHeight;
            float refVelocity = 0;

            while (startingHeight < duration)
            {
                currentHeight = Mathf.SmoothDamp(currentHeight, targetHeight, ref refVelocity, 0.3f);
                panelPresenter.SetAndUpdateDimensions(currentHeight, targetWidth);
                yield return null;
            }
        }

        #endregion Methods

    }

}

using System.Collections;
using UnityEngine;

namespace TheEvacuation.Interfaces.GameInterfaces.FlexPanel
{

    public class FlexPanelTweenAnimator : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public FlexPanelPresenter panelPresenter;

        [Range(0f, 1f)]
        public float animationSpeed = 0.5f;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void InitialiseFlexPanelTweenAnimator(FlexPanelPresenter panelPresenter)
            => this.panelPresenter = panelPresenter;

        public IEnumerator TweenToTargetDimensions(float startingHeight, float targetHeight, float statingWidth, float targetWidth)
        {
            float currentHeight = startingHeight;
            float currentWidth = statingWidth;
            float refVelocity = 0;

            while (Mathf.RoundToInt(Mathf.Abs(currentHeight - targetHeight)) > 0
                || Mathf.RoundToInt(Mathf.Abs(currentWidth - targetWidth)) > 0)
            {
                currentHeight = Mathf.SmoothDamp(currentHeight, targetHeight, ref refVelocity, animationSpeed);
                currentWidth = Mathf.SmoothDamp(currentWidth, targetWidth, ref refVelocity, animationSpeed);
                panelPresenter.SetAndUpdateDimensions(currentHeight, currentWidth);
                yield return null;
            }
        }

        #endregion Methods

    }

}

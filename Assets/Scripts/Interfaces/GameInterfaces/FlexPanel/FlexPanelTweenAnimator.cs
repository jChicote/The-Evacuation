using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace TheEvacuation.Interfaces.GameInterfaces.FlexPanel
{

    public class FlexPanelTweenAnimator : MonoBehaviour, IFlexPanelTweenAnimator
    {

        #region - - - - - - Fields - - - - - -

        public FlexPanelPresenter panelPresenter;

        [Range(0f, 1f)]
        public float animationSpeed = 0.5f;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void InitialiseFlexPanelTweenAnimator(FlexPanelPresenter panelPresenter)
            => this.panelPresenter = panelPresenter;

        public IEnumerator TweenToTargetDimensions(
            float startingHeight,
            float targetHeight,
            float statingWidth,
            float targetWidth,
            UnityAction endingAction)
        {
            float currentHeight = startingHeight;
            float currentWidth = statingWidth;
            float horizontalRefVelocity = 0;
            float verticalRefVelocity = 0;

            while (Mathf.RoundToInt(Mathf.Abs(currentHeight - targetHeight)) > 0
                || Mathf.RoundToInt(Mathf.Abs(currentWidth - targetWidth)) > 0)
            {
                currentHeight = Mathf.SmoothDamp(currentHeight, targetHeight, ref verticalRefVelocity, animationSpeed);
                currentWidth = Mathf.SmoothDamp(currentWidth, targetWidth, ref horizontalRefVelocity, animationSpeed);
                panelPresenter.SetAndUpdateDimensions(currentHeight, currentWidth);
                yield return null;
            }

            endingAction?.Invoke();
        }

        #endregion Methods

    }

}

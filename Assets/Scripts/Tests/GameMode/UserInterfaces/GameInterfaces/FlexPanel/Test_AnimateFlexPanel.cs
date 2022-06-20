using UnityEngine;

namespace TheEvacuation.Interfaces.GameInterfaces.FlexPanel
{

    public class Test_AnimateFlexPanel : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public FlexPanelPresenter flexPlanelPresenter;
        public FlexPanelTweenAnimator tweenAnimator;

        #endregion Fields

        #region - - - - - - Tests - - - - - -

        public void FlexPanel_AnimatingPanel_AnimateScPanel()
        {
            // Arrange
            tweenAnimator.InitialiseFlexPanelTweenAnimator(flexPlanelPresenter);

            // Act

            // Assert
            StartCoroutine(tweenAnimator.TweenToTargetDimensions(0, 500, 0, 500, default));
        }

        #endregion Tests

    }

}

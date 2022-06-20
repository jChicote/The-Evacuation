using System.Collections;
using UnityEngine.Events;

namespace TheEvacuation.Interfaces.GameInterfaces.FlexPanel
{

    public interface IFlexPanelTweenAnimator
    {

        #region - - - - - - Methods - - - - - -

        void InitialiseFlexPanelTweenAnimator(FlexPanelPresenter panelPresenter);

        IEnumerator TweenToTargetDimensions(float startingHeight, float targetHeight, float statingWidth, float targetWidth, UnityAction endingAction);

        #endregion Methods

    }

}

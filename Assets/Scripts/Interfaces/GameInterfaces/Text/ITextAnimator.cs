using System.Collections;
using UnityEngine.Events;

namespace TheEvacuation.Interfaces.GameInterfaces.Text
{

    public interface ITextAnimator
    {

        #region - - - - - - Methods - - - - - -

        void InitialiseTextAnimator(IDynamicTextModifier dynamicTextModifier);

        IEnumerator AnimateThroughText(string input, UnityAction action);

        #endregion Methods

    }

}



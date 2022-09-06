using UnityEngine;

namespace TheEvacuation.Interfaces.MenuInterfaces
{

    public class BaseMenuView : MonoBehaviour, IMenuView
    {

        #region - - - - - - Fields - - - - - -

        public GameObject viewElements;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void DisableViewElements()
            => viewElements.SetActive(false);

        public virtual void EnableViewElements()
            => viewElements.SetActive(true);

        #endregion Methods

    }

}

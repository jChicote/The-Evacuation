using TheEvacuation.Interfaces.MenuInterfaces.PlayerSelection;
using TheEvacuation.Interfaces.MenuInterfaces.ShipSelection;
using UnityEngine;

namespace TheEvacuation.Interfaces.MenuInterfaces.MainMenu
{

    public class MainMenuController
    {

        #region - - - - - - Fields - - - - - -

        public MainMenuView view;

        #endregion Fields

        #region - - - - - - Contructors - - - - - -

        public MainMenuController(MainMenuView view)
        {
            this.view = view;
        }

        #endregion Contructors

        #region - - - - - - Methods - - - - - -

        public void OpenShipSelection(GameObject shipSelectionView)
        {
            var selectionView = shipSelectionView.GetComponent<ISelectShipMenuView>();
            selectionView.OnViewStart();
            selectionView.EnableViewElements();
        }

        public void OpenPlayerSelection(GameObject selectionGameMenu)
        {
            Debug.Log("Selecting Player");
            var selectionView = selectionGameMenu.GetComponent<ISelectPlayerMenuView>();
            selectionView.EnableViewElements();
            selectionView.OnViewStart();
        }

        #endregion Methods

    }

}

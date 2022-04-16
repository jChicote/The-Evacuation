using TheEvacuation.Infrastructure.GameSystems;
using TheEvacuation.Infrastructure.Persistence;
using UnityEngine;

namespace TheEvacuation.Interfaces.MenuInterfaces.PlayerSelection
{

    public class OpeningMenuController
    {

        #region - - - - - - Fields - - - - - -

        private readonly UnitOfWork unitOfWork;
        private readonly OpeningMenuView view;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public OpeningMenuController(OpeningMenuView view)
        {
            this.unitOfWork = GameManager.Instance.SessionData.unitOfWork;
            this.view = view;
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public void BeginNewGame(GameObject newGameMenu)
        {
            Debug.Log("Beginning New Game");
            view.DisableViewElements();
            newGameMenu.SetActive(true);
            newGameMenu.GetComponent<IMenuView>().EnableViewElements();

            // Create a new player model for new game

            view.gameObject.SetActive(false);
        }

        public void OpenPlayerSelection(GameObject selectionGameMenu)
        {
            Debug.Log("Selecting Player");
            view.DisableViewElements();
            selectionGameMenu.SetActive(true);
            var selectionView = selectionGameMenu.GetComponent<ISelectPlayerMenuView>();
            selectionView.EnableViewElements();
            selectionView.OnViewStart();

            // Prepare model for the player selection


            view.gameObject.SetActive(false);
        }

        public void ResolveBetweenContinueAndNewGameButtons()
        {
            if (unitOfWork.Players.Entities == null)
                view.DisplayContinueButton(false);
            else
                view.DisplayContinueButton(true);
        }

        #endregion Methods
    }

}

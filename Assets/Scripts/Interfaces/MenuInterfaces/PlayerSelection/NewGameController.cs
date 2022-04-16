using TheEvacuation.Infrastructure.GameSystems;
using TheEvacuation.Infrastructure.Persistence;
using TheEvacuation.Model.Entities;
using UnityEngine;

namespace TheEvacuation.Interfaces.MenuInterfaces.PlayerSelection
{

    public class NewGameController
    {

        #region - - - - - - Fields - - - - - -

        public UnitOfWork unitOfWork;
        public Player model;
        public NewGameView view;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public NewGameController(NewGameView view)
        {
            this.unitOfWork = GameManager.Instance.SessionData.unitOfWork;
            this.view = view;

            CreateNewPlayer();
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public void CreateNewPlayer()
            => this.model = new Player();

        public void ClearNewPlayer()
        {
            this.model = null;

            view.DisableAvatarContinueButton();
            view.DisableNamingContinueButton();
        }

        public void OpenOpeningMenu(GameObject openingMenu)
        {
            view.DisableViewElements();
            openingMenu.SetActive(true);
            openingMenu.GetComponent<IMenuView>().EnableViewElements();
            model = null;

            view.gameObject.SetActive(false);
        }

        public void SelectAvatarImage(Sprite avatar)
        {
            model.avatarImage = avatar;
            view.MakeAvatarScreenContinueButtonInteractable();
        }

        public void ValidateName(string name)
        {
            if (name == null || name.Length == 0 || name.Length >= 35)
                view.DisableNamingContinueButton();
            else
                view.MakeNamingScreenContinueButtonInteractable();
        }

        public void SetPlayerName(string name)
            => model.name = name;

        #endregion Methods

    }

}

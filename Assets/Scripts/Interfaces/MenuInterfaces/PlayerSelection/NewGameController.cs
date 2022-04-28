using TheEvacuation.Infrastructure.GameSystems;
using TheEvacuation.Infrastructure.Persistence;
using TheEvacuation.Model.Entities;
using TheEvacuation.ScriptableObjects.FlyweightSettings;
using UnityEngine;

namespace TheEvacuation.Interfaces.MenuInterfaces.PlayerSelection
{

    public class NewGameController
    {

        #region - - - - - - Fields - - - - - -

        public SessionDataFacade sessionDataFacade;
        public UnitOfWork unitOfWork;
        public PlayerFlyweightSettings settings;
        public Player model;
        public NewGameView view;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public NewGameController(NewGameView view, SessionDataFacade sessionDataFacade, PlayerFlyweightSettings settings)
        {
            this.unitOfWork = GameManager.Instance.SessionData.unitOfWork;
            this.sessionDataFacade = sessionDataFacade;
            this.settings = settings;
            this.view = view;
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public void CreateNewPlayer()
        {
            SpaceShip defaultShipModel = settings.shipPrefabs[0].shipDefaults;
            SpaceShip playerShipClone = defaultShipModel.Clone();

            this.model = new Player();
            model.spaceShipHanger.Add(playerShipClone);

            Debug.Log("New Player has been created.");
        }

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

        public void FinalisePlayer()
        {
            sessionDataFacade.Player = model;
            unitOfWork.Players.Add(model);
            unitOfWork.Save();
        }

        public void SelectAvatarImage(Sprite avatar)
        {
            UserInterfaceFlyweightSettings settings = GameManager.Instance.userInterfaceFlyweightSettings;
            model.avatarIdentifier = settings.GetIdentifierFromSearchImage(avatar);
            //model.avatarImage = avatar;
            view.MakeAvatarScreenContinueButtonInteractable();
        }

        public void SetPlayerName(string name)
            => model.name = name;

        public void ValidateName(string name)
        {
            if (name == null || name.Length == 0 || name.Length >= 35)
                view.DisableNamingContinueButton();
            else
                view.MakeNamingScreenContinueButtonInteractable();
        }

        #endregion Methods

    }

}

using System;
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

        public void FinalisePlayer()
        {
            // default prefabs are alawyas at index 0 in settings

            PlayerFlyweightSettings settings = GameManager.Instance.playerFlyweightSettings;
            SpaceShip defaultShipModel = settings.shipPrefabs[0].shipDefaults;

            SpaceShip playerDefaultShip = new SpaceShip()
            {
                identifier = defaultShipModel.identifier,
                shipAttributes = new ShipAttributes()
                {
                    maxSpeed = defaultShipModel.shipAttributes.maxSpeed
                }
            };

            model.ID = Guid.NewGuid();
            model.spaceShipHanger.Add(playerDefaultShip);
            model.statistics = new PlayerStatistics()
            {
                gold = 200,
                scoreBoard = new ScoreBoard()
                {
                    totalPoints = 0,
                    highScore = 0,
                }
            };
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

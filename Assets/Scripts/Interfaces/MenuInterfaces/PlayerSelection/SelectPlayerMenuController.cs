using System;
using System.Linq;
using TheEvacuation.Infrastructure.GameSystems;
using TheEvacuation.Infrastructure.Persistence;
using TheEvacuation.Model.Entities;
using TheEvacuation.Model.ViewModels;
using TheEvacuation.ScriptableObjects.FlyweightSettings;
using UnityEngine;

namespace TheEvacuation.Interfaces.MenuInterfaces.PlayerSelection
{

    public class SelectPlayerMenuController
    {

        #region - - - - - - Fields - - - - - -

        public UserInterfaceFlyweightSettings settings;
        public SessionDataFacade sessionData;
        public UnitOfWork unitOfWork;
        public SelectPlayerMenuView view;
        public Player selectedModel;
        public Guid selectedID;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public SelectPlayerMenuController(
            SessionDataFacade sessionData,
            UserInterfaceFlyweightSettings settings,
            SelectPlayerMenuView view)
        {
            this.sessionData = sessionData;
            this.settings = settings;
            this.unitOfWork = sessionData.unitOfWork;
            this.view = view;
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public void LoadPlayerSelectionList()
        {
            if (unitOfWork.Players.Entities == null || unitOfWork.Players.Entities.Count == 0)
                return;

            foreach (var player in unitOfWork.Players.Entities)
            {
                view.CreatePlayerSelectionListCell(settings.playerSelectionCellPrefab, new PlayerCellModel()
                {
                    id = player.ID,
                    avatar = GameManager.Instance.userInterfaceFlyweightSettings.avatarImages[player.avatarIdentifier].avatarSprite,
                    name = player.name,
                    score = player.statistics.scoreBoard.totalPoints.ToString()
                });
            }
        }

        public void OpenNewPlayerGameMenu(GameObject newGameMenu)
        {
            view.DisableViewElements();
            newGameMenu.SetActive(true);
            newGameMenu.GetComponent<IMenuView>().EnableViewElements();

            view.gameObject.SetActive(false);
        }

        public void OpenOpeningMenu(GameObject openingMenu)
        {
            view.DisableViewElements();
            openingMenu.SetActive(true);
            openingMenu.GetComponent<IMenuView>().EnableViewElements();

            view.gameObject.SetActive(false);
        }

        public void OnPlay()
        {
            Player player = unitOfWork.Players.Entities
                                .Where(p => p.ID == selectedID)
                                .FirstOrDefault()
                                .Clone();

            sessionData.Player = player;
            Debug.Log("Has toggled on play");
        }

        public void OnPlayerCellSelection(Guid id)
        {
            selectedID = id;
            view.MakePlayButtonInteractable();
        }

        #endregion Methods

    }

}

using System;
using System.Linq;
using TheEvacuation.Infrastructure.GameSystems;
using TheEvacuation.Infrastructure.Persistence;
using TheEvacuation.Model.Entities;
using TheEvacuation.Model.ViewModels;
using UnityEngine;

namespace TheEvacuation.Interfaces.MenuInterfaces.PlayerSelection
{

    public class SelectPlayerMenuController
    {

        #region - - - - - - Fields - - - - - -

        public UnitOfWork unitOfWork;
        public SelectPlayerMenuView view;
        public Player selectedModel;
        public Guid selectedID;

        public Player[] playerArray; // too remove
        public GameObject testPrefab; // Too Remove

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public SelectPlayerMenuController(Player[] array, GameObject testPrefab, SelectPlayerMenuView view)
        {
            unitOfWork = GameManager.Instance.SessionData.unitOfWork;
            playerArray = array;
            this.testPrefab = testPrefab;
            this.view = view;
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public void OpenOpeningMenu(GameObject openingMenu)
        {
            view.DisableViewElements();
            openingMenu.SetActive(true);
            openingMenu.GetComponent<IMenuView>().EnableViewElements();

            view.gameObject.SetActive(false);
        }

        public void OpenNewPlayerGameMenu(GameObject newGameMenu)
        {
            view.DisableViewElements();
            newGameMenu.SetActive(true);
            newGameMenu.GetComponent<IMenuView>().EnableViewElements();

            view.gameObject.SetActive(false);
        }


        public void LoadPlayerSelectionList()
        {
            if (playerArray == null || playerArray.Length == 0)
                return;

            foreach (var player in playerArray)
            {
                view.CreatePlayerSelectionListCell(testPrefab, new PlayerCellModel()
                {
                    id = player.ID,
                    avatar = GameManager.Instance.userInterfaceFlyweightSettings.avatarImages[player.avatarIdentifier].avatarSprite,
                    name = player.name,
                    score = player.statistics.scoreBoard.totalPoints.ToString()
                });
            }
        }

        public void OnPlayerCellSelection(Guid id)
        {
            selectedID = id;
            view.MakePlayButtonInteractable();
        }

        public void OnPlay()
        {
            Player player = playerArray.Where(p => p.ID == selectedID).FirstOrDefault();
            GameManager.Instance.activePlayer = player;
            Debug.Log("Has toggled on play");
        }

        #endregion Methods

    }

}

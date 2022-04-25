using TheEvacuation.Infrastructure.GameSystems;
using TheEvacuation.Interfaces.MenuInterfaces.MainMenu;
using TheEvacuation.Model.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace TheEvacuation.Interfaces.MenuInterfaces.PlayerSelection
{

    public interface ISelectPlayerMenuView : IMenuView
    {

        #region - - - - - - Methods - - - - - -

        void OnViewStart();

        #endregion Methods

    }

    public class SelectPlayerMenuView : BaseMenuView, ISelectPlayerMenuView
    {

        #region - - - - - - Fields - - - - - -

        public Button playButton;
        public Button cancelButton;
        public SelectPlayerMenuController controller;
        public GameObject openingMenu;
        public GameObject newGameMenu;
        public GameObject elementList;
        public GameObject mainMenu;
        public GameObject playerSelectionPanel;

        #endregion Fields

        #region - - - - - - MonoBehavious - - - - - -

        void Start()
        {
            controller = new SelectPlayerMenuController
            (
                GameManager.Instance.SessionData,
                GameManager.Instance.userInterfaceFlyweightSettings,
                this
            );

            OnViewStart();
        }

        #endregion MonoBehavious

        #region - - - - - - Methods - - - - - -

        public void ClearSelectionList()
        {
            RectTransform[] childListCells = elementList.GetComponentsInChildren<RectTransform>();
            foreach (RectTransform child in childListCells)
            {
                if (child.transform.parent != elementList.transform)
                    continue; // Skips iteration.

                GameObject.Destroy(child.gameObject);
            }
        }

        public void CreatePlayerSelectionListCell(GameObject prefab, PlayerCellModel playerCellModel)
        {
            GameObject cell = Instantiate(prefab, elementList.transform);
            cell.GetComponent<IPlayerSelectionCellView>().PopulateListCell(playerCellModel, controller);
        }

        public void OnReturnToOpeningMenu()
        {
            controller.OpenOpeningMenu(openingMenu);
            ClearSelectionList(); // THIS IS LIKELY A REPEATED CALL
        }

        public void OnViewStart()
        {
            if (controller == null)
                return;

            ClearSelectionList();
            controller.LoadPlayerSelectionList();
        }

        public void OnNewPlayer()
            => controller.OpenNewPlayerGameMenu(newGameMenu);

        public void OnPlay()
        {
            controller.OnPlay();

            mainMenu.SetActive(true);
            MainMenuView mainMenuView = mainMenu.GetComponent<MainMenuView>();
            mainMenuView.EnableViewElements();
            mainMenuView.OnViewStart();

            this.gameObject.SetActive(false);
            playerSelectionPanel.SetActive(false);
        }

        public void MakePlayButtonInteractable()
            => ToggleButtonInteractivity(playButton, true);

        public void DisbalePlayButtonInteraction()
            => ToggleButtonInteractivity(cancelButton, false);

        public void ToggleButtonInteractivity(Button targetButton, bool isInteractable)
            => targetButton.interactable = isInteractable;

        #endregion Methods
    }

}
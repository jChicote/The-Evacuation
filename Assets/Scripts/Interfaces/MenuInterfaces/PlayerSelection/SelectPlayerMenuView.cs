using TheEvacuation.Model.Entities;
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

        public Player[] playerArray;
        public GameObject testPrefab;

        #endregion Fields

        #region - - - - - - MonoBehavious - - - - - -

        void Start()
        {
            controller = new SelectPlayerMenuController(playerArray, testPrefab, this);
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
            ClearSelectionList();
        }

        public void OnViewStart()
        {
            if (controller == null)
                return;

            controller.LoadPlayerSelectionList();
        }

        public void OnNewPlayer()
            => controller.OpenNewPlayerGameMenu(newGameMenu);

        public void OnPlay()
            => controller.OnPlay();

        public void MakePlayButtonInteractable()
            => ToggleButtonInteractivity(playButton, true);

        public void DisbalePlayButtonInteraction()
            => ToggleButtonInteractivity(cancelButton, false);

        public void ToggleButtonInteractivity(Button targetButton, bool isInteractable)
            => targetButton.interactable = isInteractable;

        #endregion Methods
    }

}
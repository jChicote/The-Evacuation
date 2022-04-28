using TheEvacuation.Infrastructure.GameSystems;
using TheEvacuation.Interfaces.MenuInterfaces.MainMenu;
using TheEvacuation.Model.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace TheEvacuation.Interfaces.MenuInterfaces.ShipSelection
{

    public interface ISelectShipMenuView : IMenuView
    {

        #region - - - - - - Methods - - - - - -

        void OnViewStart();

        #endregion Methods

    }

    public class ShipSelectionView : BaseMenuView, ISelectShipMenuView
    {

        #region - - - - - - Fields - - - - - -

        public ShipSelectionController controller;
        public GameObject elementList;
        public GameObject mainMenu;
        public Button playButton;
        public string sceneName;

        #endregion Fields

        #region - - - - - - MonoBehaviour - - - - - -

        private void Start()
        {
            controller = new ShipSelectionController
            (
                GameManager.Instance.playerFlyweightSettings,
                GameManager.Instance.SessionData,
                this,
                GameManager.Instance.userInterfaceFlyweightSettings
            );

            OnViewStart();
        }

        #endregion MonoBehaviour

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

        public void CreateShipSelectionListCell(GameObject prefab, ShipCellModel shipCellModel)
        {
            GameObject cell = Instantiate(prefab, elementList.transform);
            cell.GetComponent<IShipSelectionCellView>().PopulateListCell(shipCellModel, controller);
        }

        public void MakePlayButtonInteractable()
            => playButton.interactable = true;

        public void OnReturnToMainMenu()
        {
            mainMenu.SetActive(true);
            MainMenuView mainMenuView = mainMenu.GetComponent<MainMenuView>();
            mainMenuView.EnableViewElements();
            ClearSelectionList();

            this.gameObject.SetActive(false);
        }

        public void OnPlay()
        {
            controller.SetSelectedShip();

            GameManager.Instance.sceneLoader.LoadNextScene(sceneName);
            Debug.Log("Load Next Scene: " + sceneName);
        }

        public void OnViewStart()
        {
            if (controller == null)
                return;

            controller.LoadShipSelectionList();
        }

        #endregion Methods

    }

}

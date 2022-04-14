using UnityEngine;
using UnityEngine.UI;

namespace TheEvacuation.Interfaces.MenuInterfaces.PlayerSelection
{

    public interface IOpeningMenuView
    {

    }

    public class OpeningMenuView : MonoBehaviour, IOpeningMenuView
    {

        #region - - - - - - Fields - - - - - -

        public Button newGameButton;
        public Button continueAsPlayerButton;
        public Button selectPlayerButton;
        public OpeningMenuController controller;
        public GameObject selectPlayerMenu;
        public GameObject newGameMenu;
        public GameObject viewElements;

        #endregion Fields

        #region - - - - - - MonoBehaviour - - - - - -

        void Start()
        {
            controller = new OpeningMenuController(this);
            controller.ResolveBetweenContinueAndNewGameButtons();
        }

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - - -

        public void DisplayContinueButton(bool canContinue)
        {
            continueAsPlayerButton.gameObject.SetActive(canContinue);
            continueAsPlayerButton.enabled = canContinue;
            newGameButton.enabled = !canContinue;
            newGameButton.gameObject.SetActive(!canContinue);
        }

        public void DisableViewElements()
            => viewElements.SetActive(false);

        public void OnNewGame()
            => controller.BeginNewGame(this.newGameMenu);

        public void OnPlayerSelection()
            => controller.OpenPlayerSelection(this.selectPlayerMenu);

        #endregion Methods

    }

}

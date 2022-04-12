using TheEvacuation.Infrastructure.GameSystems;
using TheEvacuation.Infrastructure.Persistence;
using UnityEngine;
using UnityEngine.UI;

namespace TheEvacuation.Interfaces.MenuInterfaces.PlayerSelection
{

    public interface IOpeningMenuController
    {

    }

    public class OpeningMenuController : MonoBehaviour, IOpeningMenuController
    {

        #region - - - - - - Fields - - - - - -

        public Button newGameButton;
        public Button continueAsPlayerButton;
        public Button selectPlayerButton;
        public UnitOfWork unitOfWork;

        #endregion Fields

        #region - - - - - - MonoBehaviour - - - - - -

        // Start is called before the first frame update
        void Start()
        {
            unitOfWork = GameManager.Instance.SessionData.unitOfWork;

            ResolveBetweenContinueAndNewGameButtons();
        }

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - - -

        public void ResolveBetweenContinueAndNewGameButtons()
        {
            if (unitOfWork.Players.Entities == null)
                DisplayValidButton(newGameButton, continueAsPlayerButton);
            else
                DisplayValidButton(selectPlayerButton, continueAsPlayerButton);
        }

        public void DisplayValidButton(Button buttonToDisplay, Button buttonToHide)
        {
            buttonToDisplay.gameObject.SetActive(true);
            buttonToDisplay.enabled = true;

            buttonToHide.enabled = false;
            buttonToHide.gameObject.SetActive(false);
        }

        #endregion Methods

    }

}

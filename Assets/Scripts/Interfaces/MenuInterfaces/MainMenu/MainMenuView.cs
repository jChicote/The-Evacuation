using TheEvacuation.Infrastructure.GameSystems;
using TheEvacuation.Model.Entities;
using TheEvacuation.ScriptableObjects.FlyweightSettings;
using UnityEngine;
using UnityEngine.UI;

namespace TheEvacuation.Interfaces.MenuInterfaces.MainMenu
{

    public class MainMenuView : BaseMenuView
    {

        #region - - - - - - Fields - - - - - -

        public MainMenuController controller;
        public GameObject shipSelectionMenu;
        public GameObject playerSelectionMenu;
        public Image currentPlayerAvatar;

        #endregion Fields

        #region - - - - - - MonoBehaviour - - - - - -

        private void Start()
        {
            controller = new MainMenuController(this);
        }

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - - -

        public void OnChangePlayer()
        {
            DisableViewElements();
            playerSelectionMenu.transform.parent.gameObject.SetActive(true);
            playerSelectionMenu.SetActive(true);

            controller.OpenPlayerSelection(this.playerSelectionMenu);
            gameObject.SetActive(false);
        }

        public void OnPlay()
        {
            shipSelectionMenu.SetActive(true);
            controller.OpenShipSelection(shipSelectionMenu);
            this.gameObject.SetActive(false);
        }

        public void OnHangar() { }

        public void OnSettings() { }

        public void OnViewStart()
            => SetCurrentPlayerView();

        public void SetCurrentPlayerView()
        {
            Player currentPlayer = GameManager.Instance.SessionData.Player;
            UserInterfaceFlyweightSettings settings = GameManager.Instance.userInterfaceFlyweightSettings;
            currentPlayerAvatar.sprite = settings.avatarImages[currentPlayer.avatarIdentifier].avatarSprite;
        }

        #endregion Methods

    }

}

using System;
using TheEvacuation.Infrastructure.GameSystems;
using TheEvacuation.Interfaces.MenuInterfaces.MainMenu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace TheEvacuation.Interfaces.MenuInterfaces.PlayerSelection
{

    public class NewGameView : BaseMenuView
    {

        #region - - - - - - Fields - - - - - -

        public ScreenButtons avatarScreenButtons;
        public ScreenButtons nameScreenButtons;
        public GameObject openingMenu;
        public GameObject selectAvatarScreen;
        public GameObject nameScreen;
        public GameObject mainMenu;
        public GameObject playerSelectionPanel;
        public NewGameController controller;
        public TMP_InputField nameInputField;

        #endregion Fields

        #region - - - - - - Structs - - - - - -

        [Serializable]
        public struct ScreenButtons
        {
            public Button continueButton;
            public Button backButton;
        }

        #endregion Structs

        #region - - - - - - MonoBehaviour - - - - - -

        private void Start()
        {
            // controller = new NewGameController(this, GameManager.Instance.SessionData, GameManager.Instance.playerFlyweightSettings);
        }

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - - -

        public override void EnableViewElements()
        {
            base.EnableViewElements();
            controller = new NewGameController(this, GameManager.Instance.SessionData, GameManager.Instance.playerFlyweightSettings);
            controller.CreateNewPlayer();
        }

        public void OnReturnToOpeningMenu()
        {
            controller.OpenOpeningMenu(openingMenu);
            controller.ClearNewPlayer();
        }

        public void OnOpeningMainMenu()
        {
            controller.FinalisePlayer();

            mainMenu.SetActive(true);
            MainMenuView mainMenuView = mainMenu.GetComponent<MainMenuView>();
            mainMenuView.EnableViewElements();
            mainMenuView.OnViewStart();

            controller.ClearNewPlayer();
            this.gameObject.SetActive(false);
            this.playerSelectionPanel.SetActive(false);
        }

        public void OnNameInputFieldValueChange()
            => controller.ValidateName(nameInputField.text);

        public void OnNameInputFieldEndValueEdit()
            => controller.SetPlayerName(nameInputField.text);

        public void OpenNameScreen()
        {
            nameScreen.SetActive(true);
            selectAvatarScreen.SetActive(false);
        }

        public void OpenAvatarScreen()
        {
            nameScreen.SetActive(false);
            selectAvatarScreen.SetActive(true);
        }

        public void MakeAvatarScreenContinueButtonInteractable()
            => ToggleButtonInteractivity(avatarScreenButtons.continueButton, true);

        public void MakeNamingScreenContinueButtonInteractable()
            => ToggleButtonInteractivity(nameScreenButtons.continueButton, true);

        public void DisableNamingContinueButton()
            => ToggleButtonInteractivity(nameScreenButtons.continueButton, false);

        public void DisableAvatarContinueButton()
            => ToggleButtonInteractivity(avatarScreenButtons.continueButton, false);

        public void ToggleButtonInteractivity(Button targetButton, bool isInteractable)
            => targetButton.interactable = isInteractable;

        public void OnAvatarSelection(Sprite avatar)
            => controller.SelectAvatarImage(avatar);

        #endregion Methods

    }

}

using TheEvacuation.Interfaces.GameInterfaces.FlexPanel;
using TheEvacuation.Interfaces.GameInterfaces.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheEvacuation.Interfaces.GameInterfaces.Dialog
{

    public class DialogInteractableFlexPanelPresenter : InteractionFlexPanelPresenter
    {

        #region - - - - - - Field - - - - - -

        [Space]
        public AnimatingTextPresenter animatingTextPresenter;
        public Button panelButton;
        public TMP_Text panelText;

        [Space]
        public string[] dialogChain;

        private int indexSequence = 0;

        #endregion Field

        #region - - - - - - Methods - - - - - -

        public void DisablePanelInteraction()
            => panelButton.interactable = false;

        public void DisplayTextInSequence()
        {
            if (dialogChain == null || dialogChain.Length == 0)
            {
                Debug.LogWarning("No Dialog Text in Array.");
                return;
            }

            panelText.text = dialogChain[indexSequence];
            animatingTextPresenter.DisplayAnimatingText(dialogChain[indexSequence], EnablePanelInteraction);
            DisablePanelInteraction();
            indexSequence++;
        }

        public void EnablePanelInteraction()
            => panelButton.interactable = true;

        public override void HandlePanelExitAction()
        {
            base.HandlePanelExitAction();
            panelText.enabled = false;
            indexSequence = 0;
        }

        public override void HandlePanelAwakeAction()
        {
            if (panelAnimator == null || panelImage == null)
                return;
            base.HandlePanelAwakeAction();
            panelImage.enabled = true;
            panelText.enabled = true;
            panelText.text = "";
        }

        public override void HandlePanelStartingAction()
        {
            base.HandlePanelStartingAction();
            DisplayTextInSequence();
        }

        public override void OnPanelInteraction()
        {
            Debug.Log("IsToggled.");
            if (indexSequence < dialogChain.Length)
                DisplayTextInSequence();
            else
                HandlePanelExitAction();
        }

        #endregion Methods

    }

}

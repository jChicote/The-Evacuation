using TheEvacuation.Interfaces.GameInterfaces.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheEvacuation.Interfaces.GameInterfaces.FlexPanel.Dialog
{

    public class DialogInteractableFlexPanelPresenter : InteractionFlexPanelPresenter
    {

        #region - - - - - - Field - - - - - -

        public TMP_Text panelText;
        public AnimatingTextPresenter animatingTextPresenter;
        public Button panelButton;

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

        public override void OnDisablePanel()
        {
            base.OnDisablePanel();
            panelText.enabled = false;
        }

        public override void OnEnablePanel()
        {
            if (panelAnimator == null || panelImage == null)
                return;

            StartCoroutine(panelAnimator.TweenToTargetDimensions(startingHeight, targetHeight, startWidth, targetWidth, DisplayTextInSequence));
            panelImage.enabled = true;
            panelText.enabled = true;
            panelText.text = "";
        }

        public override void OnPanelInteraction()
        {
            Debug.Log("IsToggled.");
            if (indexSequence < dialogChain.Length)
                DisplayTextInSequence();
            else
                OnDisablePanel();
        }

        #endregion Methods

    }

}

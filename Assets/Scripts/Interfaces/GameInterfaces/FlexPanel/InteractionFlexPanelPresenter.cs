namespace TheEvacuation.Interfaces.GameInterfaces.FlexPanel
{

    public class InteractionFlexPanelPresenter : FlexPanelPresenter
    {

        #region - - - - - - Methods - - - - - -

        public virtual void OnPanelInteraction()
            => HandlePanelExitAction();

        #endregion Methods

    }

}

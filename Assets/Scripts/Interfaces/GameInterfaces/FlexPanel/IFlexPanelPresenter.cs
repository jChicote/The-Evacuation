namespace TheEvacuation.Interfaces.GameInterfaces.FlexPanel
{

    public interface IFlexPanelPresenter
    {

        #region - - - - - - Methods - - - - - -

        void HandlePanelAwakeAction();

        void HandlePanelExitAction();

        void SetAndUpdateDimensions(float height, float width);

        void UpdateTransformDimensions();

        #endregion Methods

    }

}

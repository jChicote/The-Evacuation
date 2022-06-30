namespace TheEvacuation.Interfaces.GameInterfaces.FlexPanel
{

    public interface IFlexPanelPresenter
    {

        #region - - - - - - Methods - - - - - -

        void HandlePanelExitAction();

        void HandlePanelAwakeAction();

        void SetAndUpdateDimensions(float height, float width);

        void UpdateTransformDimensions();

        #endregion Methods

    }

}

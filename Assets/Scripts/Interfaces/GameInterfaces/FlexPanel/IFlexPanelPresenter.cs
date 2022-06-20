namespace TheEvacuation.Interfaces.GameInterfaces.FlexPanel
{

    public interface IFlexPanelPresenter
    {

        #region - - - - - - Methods - - - - - -

        void OnDisablePanel();

        void OnEnablePanel();

        void SetAndUpdateDimensions(float height, float width);

        void UpdateTransformDimensions();

        #endregion Methods

    }

}

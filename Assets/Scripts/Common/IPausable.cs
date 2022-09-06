namespace TheEvacuation.Common
{

    public interface IPausable
    {

        #region - - - - - - Properties - - - - - -

        bool IsPaused { get; set; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        void OnPauseEntity();

        void OnUnpauseEntity();

        #endregion Methods

    }

}
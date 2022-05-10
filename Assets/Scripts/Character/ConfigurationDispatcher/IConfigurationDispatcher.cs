namespace TheEvacuation.Character.ConfigurationDispatcher
{
    public interface IConfigurationDispatcher<TConfigurationInputPort> where TConfigurationInputPort : class
    {

        #region - - - - - - Methods - - - - - -

        void ConfigureGameObjectSystems(TConfigurationInputPort inputPort);

        #endregion Methods

    }

}
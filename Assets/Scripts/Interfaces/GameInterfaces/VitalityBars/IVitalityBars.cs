namespace TheEvacuation.Interfaces.GameInterfaces.VitalityBars
{
    public interface IVitalityBars
    {

        #region - - - - - - Methods - - - - - -

        float GetBarValue();

        void SetBarValue(float value, float maximum);

        #endregion Methods

    }

}

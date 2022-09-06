namespace TheEvacuation.Interfaces.GameInterfaces.Text
{
    public interface IDynamicTextModifier
    {

        #region - - - - - - Methods - - - - - -

        string GetTextValue();

        void SetTextValue(string newText);

        #endregion Methods

    }

}
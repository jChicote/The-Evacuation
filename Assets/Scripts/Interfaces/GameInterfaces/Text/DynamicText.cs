using TMPro;
using UnityEngine;

namespace TheEvacuation.Interfaces.GameInterfaces.Text
{

    public class DynamicText : MonoBehaviour, IDynamicTextModifier
    {

        #region - - - - - - Fields - - - - - -

        public TMP_Text textLabel;

        public string prefix = "";
        public string suffix = "";

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public string GetTextValue()
            => textLabel.text;

        public void SetTextValue(string newText)
            => textLabel.text = prefix + newText + suffix;

        #endregion Methods
    }

}
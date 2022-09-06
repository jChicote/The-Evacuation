using TheEvacuation.Interfaces.GameInterfaces.Text;
using UnityEngine;

namespace TheEvacuation.Tests.GameMode.UserInterfaces.GameInterfaces.Text
{

    public class Test_DynamicTest : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public string testString;
        public DynamicText dynamicLabel;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void SetTextValue_SetValueToInputString_SetsLabelValue()
        {
            // Arrange

            // Act
            dynamicLabel.SetTextValue(testString);

            // Assert
            Debug.Log("Label equals test string: " + (testString.Equals(dynamicLabel.GetTextValue())));
        }

        #endregion Methods

    }

}

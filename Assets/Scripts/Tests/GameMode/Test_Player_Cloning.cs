using TheEvacuation.Model.Entities;
using UnityEngine;

namespace TheEvacuation.Tests.GameMode
{

    public class Test_Player_Cloning : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public Player player;
        public Player clonePlayer;

        #endregion Fields

        #region - - - - - - Clone Tests - - - - - -

        /// <summary>
        /// Will clone the player object but equality judged in editor.
        /// </summary>
        public void Clone_PlayerHasDataToClone_CLonedReplicateMatches()
        {
            // Arrange

            // Act
            var clone = player.Clone();
            clonePlayer = clone;
        }

        #endregion Clone Tests

    }

}
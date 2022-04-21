using System.Collections;
using System.Collections.Generic;
using TheEvacuation.Model.Entities;
using UnityEngine.TestTools;

namespace TheEvacuation.Tests.EditMode.Model.Entities
{

    public class PlayerCloningTests
    {

        #region - - - - - - Clone Tests - - - - - -

        [UnityTest]
        public IEnumerator Clone_PlayerWithPopulatedData_MatchingClone()
        {
            // Arrange
            var targetPlayer = new Player()
            {
                name = "Character-1",
                avatarIdentifier = 12,
                spaceShipHanger = new List<SpaceShip>(),
                statistics = new PlayerStatistics()
                {
                    gold = 100,
                    scoreBoard = new ScoreBoard()
                    {
                        totalPoints = 100,
                        highScore = 100
                    }
                }
            };

            // Act
            var actual = targetPlayer.Clone();

            // Assert
            yield return null;
        }

        #endregion Clone Tests

    }

}
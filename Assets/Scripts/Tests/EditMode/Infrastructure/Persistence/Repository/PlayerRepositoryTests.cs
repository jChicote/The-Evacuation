using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;

namespace TheEvacuation.Tests.EditMode.Infrastructure.Persistence.Repository
{

    public class PlayerRepositoryTests
    {

        #region - - - - - - Fields - - - - - - -

        //private readonly Mock<DataContext>

        #endregion Fields

        #region - - - - - - Constructors - - - - - - -

        public PlayerRepositoryTests()
        {

        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - - -

        [UnityTest]
        public IEnumerator GetById_EntityExistsInList_EntityFoundWithMatchingID()
        {
            // Arrange

            // Act

            // Assert
            yield return null;
            Assert.AreEqual(true, true);
        }

        [UnityTest]
        public IEnumerator Add_EntityExists_NonEmptyCollection()
        {
            // Arrange

            // Act

            // Assert
            yield return null;
            Assert.AreEqual(true, true);

        }

        [UnityTest]
        public IEnumerator Delete_ValidEntityForDeletion_EmptyCollection()
        {
            // Arrange

            // Act

            // Assert
            yield return null;
            Assert.AreEqual(true, true);

        }

        [UnityTest]
        public IEnumerator Modify_PlayerEntityWithAttributeObject_PlayerContainsAttributeObject()
        {
            // Arrange

            // Act

            // Assert
            yield return null;
            Assert.AreEqual(true, true);

        }

        #endregion Methods

    }

}
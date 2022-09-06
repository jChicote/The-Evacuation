using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TheEvacuation.Infrastructure.Persistence;
using TheEvacuation.Infrastructure.Persistence.Repository;
using TheEvacuation.Model.Entities;
using UnityEngine.TestTools;

namespace TheEvacuation.Tests.EditMode.Infrastructure.Persistence.Repository
{

    public class PlayerRepositoryTests
    {

        #region - - - - - - Fields - - - - - - -

        private readonly Mock<IDataContext> m_MockJsonDataContext = new Mock<IDataContext>();

        private readonly Player m_Player = new Player() { ID = Guid.NewGuid() };
        private readonly PlayerRepository m_PlayerRepository = new PlayerRepository();
        private List<Player> m_Players;

        #endregion Fields

        #region - - - - - - Constructors - - - - - - -

        public PlayerRepositoryTests()
        {
            m_PlayerRepository.context = m_MockJsonDataContext.Object;
            m_Players = new List<Player>() { m_Player };

            _ = m_MockJsonDataContext
                    .Setup(mock => mock.Set<Player>())
                    .Returns(m_Players);
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - - -

        [UnityTest]
        public IEnumerator GetById_EntityExistsInList_EntityFoundWithMatchingID()
        {
            // Arrange
            m_Players = new List<Player>() { m_Player };
            _ = m_MockJsonDataContext
                   .Setup(mock => mock.Set<Player>())
                   .Returns(m_Players);

            // Act
            var actual = m_PlayerRepository.GetById(m_Player.ID);

            // Assert
            yield return null;
            Assert.AreEqual(m_Player, actual);
        }

        [UnityTest]
        public IEnumerator GetById_EmptyCollection_EntityNotFound()
        {
            // Arrange
            m_Players = new List<Player>() { };
            _ = m_MockJsonDataContext
                   .Setup(mock => mock.Set<Player>())
                   .Returns(m_Players);

            // Act
            var actual = m_PlayerRepository.GetById(m_Player.ID);

            // Assert
            yield return null;
            Assert.AreEqual(default, actual);
        }

        [UnityTest]
        public IEnumerator Add_EntityExists_NonEmptyCollection()
        {
            // Arrange

            // Act
            m_PlayerRepository.Add(m_Player);

            //// Assert
            yield return null;
            m_MockJsonDataContext.Verify(mock => mock.Set<Player>(), Times.Once);
        }

        [UnityTest]
        public IEnumerator Delete_ValidEntityForDeletion_EmptyCollection()
        {
            // Arrange
            //m_Player.PlayerAttributes = new PlayerAttributes();

            //// Act
            //m_PlayerRepository.Delete(m_Player);

            //// Assert
            yield return null;
            //m_MockJsonDataContext.Verify(mock => mock.Set<Player>(), Times.Once);
        }

        //[UnityTest]
        //public IEnumerator Modify_PlayerEntityWithAttributeObject_PlayerContainsAttributeObject()
        //{
        //    // Arrange

        //    // Act
        //    m_PlayerRepository.Modify(m_Player);

        //    // Assert
        //    yield return null;
        //    m_MockJsonDataContext.Verify(mock => mock.Set<Player>(), Times.Once);
        //}

        #endregion Methods

    }

}
using System;
using TheEvacuation.Infrastructure.GameSystems;
using TheEvacuation.Infrastructure.Persistence;
using TheEvacuation.Model.Entities;
using UnityEngine;

namespace TheEvacuation.Tests.GameMode
{

    public class Test_BackEndTriggers : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public SessionDataFacade sessionData;
        public UnitOfWork unitOfWork;

        #endregion Fields

        #region - - - - - - Load Tests - - - - - -

        public void Load_FileWhenEmpty_ThrowsErrorException()
        {
            // Ensure that the file name is different from intended game file for usage.
            unitOfWork.Load();
        }

        public void Load_LoadFromRecentlyNewSavedFile_NoErrorsThrown()
        {
            // Arrange
            unitOfWork.Save();

            // Act
            unitOfWork.Load();
        }

        public void Load_CreateNewGameDataObject_GameDataExistsAfterLoad()
        {
            // Arrange
            var dataContext = unitOfWork.GetComponent<IDataContext>();
            dataContext.CreateNewGameData();
            unitOfWork.Save();

            // Act
            unitOfWork.Load();

            // Assert
            //Debug.Log(unitOfWork.Players.);
            bool doesEntitiesExist = unitOfWork.Players.Entities != null;
            Debug.Log("Does Player exist after loading: " + doesEntitiesExist);
        }

        public void Load_AddPlayerToSaveFile_PlayerExistsInFile()
        {
            // Arrange
            var dataContext = unitOfWork.GetComponent<IDataContext>();
            dataContext.CreateNewGameData();

            var playerID = Guid.NewGuid();
            var createdPlayer = new Player() { name = "Jaiden Chicote", ID = playerID };
            unitOfWork.Players.Add(createdPlayer);
            unitOfWork.Save();

            // Act
            unitOfWork.Load();

            // Assert
            bool doesPlayerExist = unitOfWork.Players.GetById(playerID) != null;
            Debug.Log("Does Player exist after loading: " + doesPlayerExist); // Expects: True
            Debug.Log("Count in Players: " + unitOfWork.Players.Entities.Count); // Expects: 1
        }

        public void Load_RemovePlayerFromSaveFile_PlayerRepositoryListIsEmpty()
        {
            // Arrange
            var dataContext = unitOfWork.GetComponent<IDataContext>();
            dataContext.CreateNewGameData();

            var playerID = Guid.NewGuid();
            var createdPlayer = new Player() { name = "Jaiden Chicote", ID = playerID };
            unitOfWork.Players.Add(createdPlayer);
            unitOfWork.Save();
            unitOfWork.Players.Delete(createdPlayer);
            unitOfWork.Save();

            // Act
            unitOfWork.Load();

            // Assert
            Debug.Log("Count in Players: " + unitOfWork.Players.Entities.Count); // Expects: 0
        }

        public void Load_ModifyPlayerAttributes_PlayerWithSameIDButDifferentName()
        {
            // Arrange
            var dataContext = unitOfWork.GetComponent<IDataContext>();
            dataContext.CreateNewGameData();

            var playerID = Guid.NewGuid();
            var createdPlayer = new Player() { name = "Jaiden Chicote", ID = playerID };
            unitOfWork.Players.Add(createdPlayer);
            unitOfWork.Save();
            Debug.Log("Player Name Before:  " + unitOfWork.Players.GetById(playerID).name); // Expects: Jaiden Chicote

            unitOfWork.Players.GetById(playerID).name = "Senki";
            unitOfWork.Players.Modify(createdPlayer);
            unitOfWork.Save();

            // Act
            //dataContext.CreateNewGameData();
            unitOfWork.Load();

            // Assert
            Debug.Log("Player Name After: " + unitOfWork.Players.GetById(playerID).name); // Expects: Senki
        }

        #endregion Load Tests

    }

}

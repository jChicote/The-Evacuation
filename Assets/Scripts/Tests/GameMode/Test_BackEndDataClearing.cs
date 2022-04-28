using TheEvacuation.Infrastructure.GameSystems;
using TheEvacuation.Infrastructure.Persistence;
using UnityEngine;

namespace TheEvacuation.Tests.GameMode
{

    public class Test_BackEndDataClearing : MonoBehaviour
    {
        public SessionDataFacade sessionData;

        public void ClearAllData()
        {
            UnitOfWork unitOfWork = sessionData.unitOfWork;
            unitOfWork.Players.Entities.Clear();
            unitOfWork.Save();

            Debug.LogWarning("All Data Has Been Cleared !!!!!!!!!");
        }
    }

}

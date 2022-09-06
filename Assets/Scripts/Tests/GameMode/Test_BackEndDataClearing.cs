using TheEvacuation.Infrastructure.GameSystems;
using TheEvacuation.Infrastructure.Persistence;
using UnityEngine;

namespace TheEvacuation.Tests.GameMode
{

    public class Test_BackEndDataClearing : MonoBehaviour
    {
        public SessionData sessionData;

        public void ClearAllData()
        {
            UnitOfWork unitOfWork = sessionData;
            unitOfWork.Players.Entities.Clear();
            unitOfWork.Save();

            Debug.LogWarning("All Data Has Been Cleared !!!!!!!!!");
        }
    }

}

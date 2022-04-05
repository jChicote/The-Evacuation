using TheEvacuation.Infrastructure.Persistence;
using TheEvacuation.Model.Entities;
using UnityEngine;

namespace TheEvacuation.Infrastructure.GameSystems
{

    public class SessionDataFacade : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public UnitOfWork unitOfWork;

        public Player currentPlayer;
        public ActiveShip activeShip;

        #endregion Fields

    }

}

using TheEvacuation.Infrastructure.Persistence;
using TheEvacuation.Model.Entities;
using UnityEngine;

namespace TheEvacuation.Infrastructure.GameSystems
{

    public class SessionDataFacade : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public UnitOfWork unitOfWork;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public Player Player { get; set; }
        public SpaceShip SelectedSpaceShip { get; set; }
        public ScoreBoard PlayerScoreBoard { get; set; }

        #endregion Properties

    }

}

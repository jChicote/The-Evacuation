using TheEvacuation.Infrastructure.Persistence;
using TheEvacuation.Model.Entities;

namespace TheEvacuation.Infrastructure.GameSystems
{

    public class SessionData : UnitOfWork
    {

        #region - - - - - - Properties - - - - - -

        public Player CurrentPlayer { get; set; }
        public SpaceShip SelectedSpaceShip { get; set; }
        public ScoreBoard PlayerScoreBoard { get; set; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public void SaveAllData()
        {
            Players.Modify(CurrentPlayer);
            Save();
        }

        #endregion

    }

}

using System.Collections.Generic;
using TheEvacuation.Model.Entities;

namespace TheEvacuation.Infrastructure.Persistence
{

    public class GameData
    {

        #region - - - - - - Fields - - - - - -

        public List<Level> m_Levels;
        public List<Player> m_Players;
        public List<ScoreBoard> m_ScoreBoards;

        #endregion Fields

    }

}

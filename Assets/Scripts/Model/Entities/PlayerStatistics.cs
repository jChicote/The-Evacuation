using System;

namespace TheEvacuation.Model.Entities
{

    [Serializable]
    public class PlayerStatistics
    {

        #region - - - - - - Fields - - - - - -

        public Guid ID;
        public int gold;
        public ScoreBoard scoreBoard;

        #endregion Fields

    }

}
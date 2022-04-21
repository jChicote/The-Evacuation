using System;

namespace TheEvacuation.Model.Entities
{

    [Serializable]
    public class PlayerStatistics : ICloneable<PlayerStatistics>
    {

        #region - - - - - - Fields - - - - - -

        public Guid ID;
        public int gold;
        public ScoreBoard scoreBoard;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public PlayerStatistics Clone()
        {
            PlayerStatistics clone = ShallowClone();
            clone.scoreBoard = scoreBoard.Clone();
            return clone;
        }

        public PlayerStatistics ShallowClone()
            => this.MemberwiseClone() as PlayerStatistics;

        #endregion Methods

    }

}
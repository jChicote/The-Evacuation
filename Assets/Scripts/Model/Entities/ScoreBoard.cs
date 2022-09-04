using System;

namespace TheEvacuation.Model.Entities
{

    /// <summary>
    /// Stores the score data from each level.
    /// <para>
    /// This entity can be overriden by boards created within each level scene.
    /// Only stores calculated data and stored inside the data context.
    /// </para>
    /// </summary>
    [System.Serializable]
    public class ScoreBoard : BaseEntity, ICloneable<ScoreBoard>
    {

        #region - - - - - - Fields - - - - - -

        public int totalPoints = 0;
        public int highScore = 0;
        public int totalDeaths = 0;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public ScoreBoard()
        {
            ID = new Guid();
            totalPoints = 0;
            highScore = 0;
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public ScoreBoard Clone()
        {
            ScoreBoard clone = ShallowClone();
            return clone;
        }

        public ScoreBoard ShallowClone()
            => this.MemberwiseClone() as ScoreBoard;

        #endregion Methods

    }

}

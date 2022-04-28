namespace TheEvacuation.Model.Entities
{

    [System.Serializable]
    public class ScoreBoard : ICloneable<ScoreBoard>
    {

        #region - - - - - - Fields - - - - - -

        public int totalPoints = 0;
        public int highScore = 0;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public ScoreBoard()
        {
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

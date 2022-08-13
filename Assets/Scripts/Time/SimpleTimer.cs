namespace TheEvacuation.TimeUtility
{

    public class SimpleCountDown
    {

        #region - - - - - - Fields - - - - - -

        private float intervalLength;
        private float timeLeft;
        private float deltaTime;
        private float interpolateValue;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public float TimeLeft { get => timeLeft; }
        public float InterpolateValue { get => interpolateValue; }

        #endregion Properties

        #region - - - - - - Constructors - - - - - -

        public SimpleCountDown(float intervalLength, float deltaTime)
        {
            this.intervalLength = intervalLength;
            this.timeLeft = intervalLength;
            this.deltaTime = deltaTime;
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public void TickTimer()
        {
            timeLeft -= deltaTime;
            interpolateValue = 1 - timeLeft / intervalLength;
        }

        public bool CheckTimeIsUp()
            => timeLeft <= 0;

        public void ResetTimer()
            => timeLeft = intervalLength;

        #endregion

    }

}

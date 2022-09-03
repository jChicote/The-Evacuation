using TheEvacuation.Infrastructure.GameSystems.SceneSystems;
using TheEvacuation.Interfaces.GameInterfaces.Score;

namespace TheEvacuation.Infrastructure.Score.UpdateScoreRecord
{

    public class UpdateScoreRecordController : IUpdateScoreRecord
    {

        #region - - - - - - Fields - - - - - -

        //private UserInterfaceFlyweightSettings uiSettings; TODO: Implement UI hover items
        private readonly IScorePointsPresenter scorePointsPresenter;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public UpdateScoreRecordController(IScorePointsPresenter scorePointsPresenter)
        {
            this.scorePointsPresenter = scorePointsPresenter;
            //this.uiSettings = uiSettings;
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public void UpdateScoreRecord(ScoreRecord scoreRecord, ScoreEvent scoreEvent)
        {
            scoreRecord.KillCount++;
            scoreRecord.TotalScore += scoreEvent.ScoreValue;
            scorePointsPresenter.PresentScore(scoreRecord.TotalScore);

            // Present popup items
        }
        //public void CreatePopup(ScoreEvent scoreEvent) {  }

        #endregion Methods

    }

    public enum ScoreEventType
    {
        Death,
        Kill,
        Hit,
        None
    }

}
using TheEvacuation.Infrastructure.GameSystems.SceneSystems;

namespace TheEvacuation.Infrastructure.Score.UpdateScoreRecord
{
    public interface IUpdateScoreRecord
    {

        #region - - - - - - Methods - - - - - -

        void UpdateScoreRecord(ScoreRecord scoreRecord, ScoreEvent scoreEvent);

        #endregion Methods

    }

}
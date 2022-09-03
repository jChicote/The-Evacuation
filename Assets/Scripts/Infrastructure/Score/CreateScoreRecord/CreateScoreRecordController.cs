using TheEvacuation.Infrastructure.GameSystems.SceneSystems;
using UnityEngine;

namespace TheEvacuation.Infrastructure.Score.CreateScoreRecord
{

    public interface ICreateScoreRecord
    {

        #region - - - - - - Methods - - - - - -

        ScoreRecord CreateScoreRecord();

        #endregion Methods

    }

    public class CreateScoreRecordController : MonoBehaviour, ICreateScoreRecord
    {

        #region - - - - - - Methods - - - - - -

        public ScoreRecord CreateScoreRecord()
            => new ScoreRecord()
            {
                TotalScore = 0,
            };

        #endregion Methods

    }

}

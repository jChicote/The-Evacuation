using TheEvacuation.Infrastructure.GameSystems.SceneSystems;
using UnityEngine;

namespace TheEvacuation.Infrastructure.Score.UpdateScoreRecord
{

    public interface IUpdateScoreRecord
    {

        #region - - - - - - Methods - - - - - -

        void UpdateScoreRecord(ScoreRecord scoreRecord, ScoreEvent scoreEvent);

        #endregion Methods

    }

    public class UpdateScoreRecordController : MonoBehaviour, IUpdateScoreRecord
    {

        #region - - - - - - Fields - - - - - -

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void UpdateScoreRecord(ScoreRecord scoreRecord, ScoreEvent scoreEvent)
        {

        }

        #endregion Methods

    }

}
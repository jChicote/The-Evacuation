using TheEvacuation.Infrastructure.Score.CreateScoreRecord;
using TheEvacuation.Infrastructure.Score.UpdateScoreRecord;
using TheEvacuation.Interfaces.GameInterfaces.Score;
using UnityEngine;

namespace TheEvacuation.Infrastructure.GameSystems.SceneSystems
{

    public class SceneScoreSystem : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public IScorePresenter scorePresenter;
        public ICreateScoreRecord createScoreRecord;
        public IUpdateScoreRecord updateScoreRecord;

        private ScoreRecord scoreRecord;

        #endregion Fields

        #region - - - - - - MonoBehaviour - - - - - -

        private void Start()
        {

        }

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - - -

        /*
         *
         * What needs to be in here:
         *
         * - An interface method to update score on event of death or update
         * - Initailisation by scene operations pipeline
         * - Communication through score manager through usage of view models (keeps components seperated)
         * - Operate idenpendantely nor coupled with any entity in the scene.
         *
         */

        public void InitialiseSceneScoreSystem(IScorePresenter scorePresenter)
        {
            this.scorePresenter = scorePresenter;
            this.scoreRecord = this.createScoreRecord.CreateScoreRecord();
        }

        public void UpdateTotalScore(int scoreValue)
        {
            this.scoreRecord.TotalScore += scoreValue;
            scorePresenter.PresentScore(this.scoreRecord.TotalScore);
        }

        #endregion Methods

    }

    public class ScoreRecord
    {

        #region - - - - - - Properties - - - - - -

        public int TotalScore { get; set; }

        #endregion Properties

    }

    public class ScoreEvent
    {

        #region - - - - - - Properties - - - - - -

        public int ScoreValue { get; set; }

        public GameObject ScoreSubscriber { get; set; }

        #endregion Properties

    }

}

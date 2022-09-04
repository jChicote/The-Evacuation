using System;
using TheEvacuation.Infrastructure.Score.CreateScoreRecord;
using TheEvacuation.Infrastructure.Score.LoadScoreBoard;
using TheEvacuation.Infrastructure.Score.UpdateScoreRecord;
using TheEvacuation.Interfaces.GameInterfaces.Score;
using TheEvacuation.Model.Entities;
using UnityEngine;

namespace TheEvacuation.Infrastructure.GameSystems.SceneSystems
{

    public class SceneScoreSystem : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public IScorePointsPresenter scorePresenter;
        public ICreateScoreRecord createScoreRecord;
        public IUpdateScoreRecord updateScoreRecord;
        public ILoadScoreBoard loadScoreBoard;

        private ScoreRecord scoreRecord;
        private ScoreBoard scoreBoard;

        #endregion Fields

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

        public void InitialiseSceneScoreSystem(IScorePointsPresenter scorePresenter)
        {
            this.scorePresenter = scorePresenter;

            this.createScoreRecord = new CreateScoreRecordController();
            this.updateScoreRecord = new UpdateScoreRecordController(scorePresenter);
            this.loadScoreBoard = new LoadScoreBoardController();

            this.scoreRecord = this.createScoreRecord.CreateScoreRecord();
        }

        /// <summary>
        /// Loads existing scoreboard, else create scoreboard
        /// </summary>
        public void LoadScoreBoard(Guid scoreBoardID)
            => scoreBoard = this.loadScoreBoard.LoadScoreBoard(scoreBoardID);

        public void SaveScoreRecord() { }

        public void UpdateTotalScore(ScoreEvent scoreEvent)
            => this.updateScoreRecord.UpdateScoreRecord(scoreRecord, scoreEvent);

        public ScoreRecord GetScoreRecord()
            => this.scoreRecord;

        #endregion Methods

    }

    /// <summary>
    /// Tracks the score game data during scene gameplay.
    /// </summary>
    public class ScoreRecord
    {

        #region - - - - - - Properties - - - - - -

        public int KillCount { get; set; }

        public int SkillPoints { get; set; }

        public int TotalScore { get; set; }

        #endregion Properties

    }

    /// <summary>
    /// Represents an event containing the score sent by a subscribing game entity.
    /// </summary>
    public class ScoreEvent
    {

        #region - - - - - - Properties - - - - - -

        public int ScoreValue { get; set; }

        public ScoreEventType EventType { get; set; }

        public ScoreSubscriber ScoreSubscriber { get; set; }

        #endregion Properties

    }

    public class ScoreSubscriber
    {

        #region - - - - - - Properties - - - - - -

        public string Name { get; set; }

        public Transform Transform { get; set; }

        #endregion Properties

    }



}

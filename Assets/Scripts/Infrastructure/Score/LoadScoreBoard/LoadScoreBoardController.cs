using System;
using TheEvacuation.Infrastructure.GameSystems;
using TheEvacuation.Model.Entities;

namespace TheEvacuation.Infrastructure.Score.LoadScoreBoard
{

    public interface ILoadScoreBoard
    {

        #region - - - - - - Methods - - - - - -

        ScoreBoard LoadScoreBoard(Guid scoreBoardID);

        #endregion Methods

    }

    public class LoadScoreBoardController : ILoadScoreBoard
    {

        #region - - - - - - Methods - - - - - -

        public ScoreBoard LoadScoreBoard(Guid scoreBoardID)
        {
            // Loads dummy ID
            ScoreBoard scoreBoard = GameManager.Instance.SessionData.ScoreBoard.GetById(new System.Guid())
                                        ?? new ScoreBoard();
            return scoreBoard;
        }

        #endregion Methods

    }

}
using TheEvacuation.Model.Entities;
using UnityEngine;

namespace TheEvacuation.Infrastructure.GameSystems
{

    public class ScoreViewModel
    {

        #region - - - - - - Properties - - - - - -

        public int TotalScore;

        #endregion Properties

    }

    public class ScoreManager : MonoBehaviour
    {

        #region - - - - - - Properties - - - - - -

        public ScoreBoard PlayerScoreBoard { get; private set; }

        #endregion Properties

        #region - - - - - - MonoBehaviour - - - - - -

        private void Awake()
        {
            if (GameObject.FindObjectOfType<ScoreManager>() == null)
                DontDestroyOnLoad(gameObject);
            else
                Destroy(gameObject);
        }

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - - -

        public void InitialiseScoreManager(ScoreBoard scoreBoard)
        {
            PlayerScoreBoard = scoreBoard;
        }

        public void UpdateScore(ScoreViewModel scoreVM)
        {
            PlayerScoreBoard.totalPoints += scoreVM.TotalScore;
            // Sync scoreboard
        }

        #endregion Methods

    }

}

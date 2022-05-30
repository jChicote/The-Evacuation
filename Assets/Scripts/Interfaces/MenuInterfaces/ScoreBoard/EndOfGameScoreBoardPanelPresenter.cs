using TheEvacuation.Model.ViewModels;
using UnityEngine;

namespace TheEvacuation.Interfaces.MenuInterfaces.ScoreBoard
{

    public class EndOfGameScoreBoardPanelPresenter : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public IncrementingLabel currentScoreLabel;
        public IncrementingLabel topScoreLabel;

        public ScoreboardViewModel scoreboardViewModel;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void InitialiseEndOfGameScoreBoardPanelPresenter(ScoreboardViewModel scoreBoardViewModel)
        {
            // Trigger score display
            currentScoreLabel.DisplayScoreNumberOverTime(scoreBoardViewModel.CurrentScore);
            topScoreLabel.DisplayScoreNumberOverTime(scoreBoardViewModel.TotalScore);
        }

        #endregion Methods

    }

}
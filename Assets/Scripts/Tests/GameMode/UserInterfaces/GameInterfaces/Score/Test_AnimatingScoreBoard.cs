using TheEvacuation.Interfaces.MenuInterfaces.ScoreBoard;
using TheEvacuation.Model.ViewModels;
using UnityEngine;

public class Test_AnimatingScoreBoard : MonoBehaviour
{

    #region - - - - - - Fields - - - - - -

    public EndOfGameScoreBoardPanelPresenter presenter;

    #endregion Fields

    #region - - - - - - Tests - - - - - -

    public void InitialiseEndOfGameScoreBoardPanelPresenter_ValidViewModel_AnimateScore()
    {
        // Arrange
        ScoreboardViewModel viewModel = new ScoreboardViewModel()
        {
            CurrentScore = 90100124,
            TotalScore = 192000120
        };

        // Act
        presenter.InitialiseEndOfGameScoreBoardPanelPresenter(viewModel);

        // Assert
    }

    #endregion Tests

}

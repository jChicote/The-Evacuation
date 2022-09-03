using TheEvacuation.Interfaces.GameInterfaces.Text;
using UnityEngine;

namespace TheEvacuation.Interfaces.GameInterfaces.Score
{

    public class ScorePointsPresenter : MonoBehaviour, IScorePointsPresenter
    {

        #region - - - - - - Fields - - - - - -

        public DynamicText scoreLabel;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void PresentScore(int score)
            => scoreLabel.SetTextValue(score.ToString());

        #endregion Methods

    }

}

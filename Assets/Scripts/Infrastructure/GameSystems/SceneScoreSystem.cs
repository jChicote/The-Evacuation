using TheEvacuation.Interfaces.GameInterfaces.Score;
using UnityEngine;

namespace TheEvacuation.Infrastructure.GameSystems
{

    public class SceneScoreSystem : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public IScorePresenter scorePresenter;

        public int totalScore;

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
        }

        public void UpdateTotalScore(int scoreValue)
        {
            totalScore += scoreValue;
            scorePresenter.PresentScore(totalScore);
        }

        #endregion Methods

    }

}

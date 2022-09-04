using TheEvacuation.Model.Entities;

namespace TheEvacuation.Infrastructure.Persistence.Repository
{

    public class ScoreBoardRepository : Repository<ScoreBoard>
    {

        #region - - - - - - Methods - - - - - -

        private void Awake()
            => context = this.GetComponent<IDataContext>();

        #endregion Methods

    }

}

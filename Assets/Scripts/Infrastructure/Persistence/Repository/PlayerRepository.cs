using TheEvacuation.Model.Entities;

namespace TheEvacuation.Infrastructure.Persistence.Repository
{

    public class PlayerRepository : Repository<Player>
    {

        #region - - - - - - Methods - - - - - -

        private void Awake()
            => context = this.GetComponent<IDataContext>();

        #endregion Methods

    }

}

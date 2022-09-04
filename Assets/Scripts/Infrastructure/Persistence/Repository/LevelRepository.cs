using TheEvacuation.Model.Entities;

namespace TheEvacuation.Infrastructure.Persistence.Repository
{

    public class LevelRepository : Repository<Level>
    {

        #region - - - - - - Methods - - - - - -

        private void Awake()
            => context = this.GetComponent<IDataContext>();

        #endregion Methods

    }

}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheEvacuation.Infrastructure.Persistence
{

    public interface IDataContext
    {

        #region - - - - - - Methods - - - - - -

        public abstract Task Load();

        public abstract Task Save();

        public List<TBase> Set<TBase>();

        #endregion Methods

    }

}

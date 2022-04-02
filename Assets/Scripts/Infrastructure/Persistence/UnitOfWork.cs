using TheEvacuation.Infrastructure.Persistence.Repository;
using UnityEngine;

namespace TheEvacuation.Infrastructure.Persistence
{

    public class UnitOfWork : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private DataContext dataContext;
        [SerializeField] private PlayerRepository playerRepository;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public PlayerRepository Players => playerRepository;

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public async void Load() => await dataContext.Load();

        public async void Save() => await dataContext.Save();

        #endregion Methods

    }

}

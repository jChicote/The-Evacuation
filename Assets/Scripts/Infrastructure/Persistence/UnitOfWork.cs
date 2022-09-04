using TheEvacuation.Infrastructure.Persistence.Repository;
using UnityEngine;

namespace TheEvacuation.Infrastructure.Persistence
{

    public class UnitOfWork : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private DataContext dataContext;
        [SerializeField] private LevelRepository levelRepository;
        [SerializeField] private PlayerRepository playerRepository;
        [SerializeField] private ScoreBoardRepository scoreBoardRepository;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public LevelRepository LevelRepository => levelRepository;

        public PlayerRepository Players => playerRepository;

        public ScoreBoardRepository ScoreBoard => scoreBoardRepository;

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public async void Load() => await dataContext.Load();

        public async void Save() => await dataContext.Save();

        #endregion Methods

    }

}

using TheEvacuation.Model.Entities;

namespace TheEvacuation.Spawner
{
    public interface IPlayerSpawner : ISpawner
    {

        #region - - - - - - Methods - - - - - -

        void IntialisePlayerSpawner(SpaceShip spaceShip);

        #endregion Methods

    }

}

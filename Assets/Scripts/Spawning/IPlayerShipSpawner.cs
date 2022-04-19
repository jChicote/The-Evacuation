using TheEvacuation.Model.Entities;

namespace TheEvacuation.Spawner
{
    public interface IPlayerShipSpawner : ISpawner
    {

        #region - - - - - - Methods - - - - - -

        void IntialisePlayerSpawner(SpaceShip spaceShip);

        #endregion Methods

    }

}

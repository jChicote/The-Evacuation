using TheEvacuation.Interfaces.GameInterfaces.VitalityBars;
using TheEvacuation.Model.Entities;

namespace TheEvacuation.PlayerSystems.Vitality
{
    public interface IPlayerHealthSystem
    {

        #region - - - - - - Methods - - - - - -

        void InitialisePlayerHealthSystem(IPlayerHealthBar healthBar, ShipAttributes shipAttributes);

        #endregion Methods

    }

}

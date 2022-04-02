using UnityEngine;

namespace TheEvacuation.PlayerSystems.Movement
{
    public interface ICharacterMovement
    {

        #region - - - - - - Properties - - - - - -

        bool IsMovementHeld { get; set; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        void CalculateMovement(Vector2 normalisedDirection);

        void CalculateShipRotation(Vector2 startPos, Vector2 endPos);

        #endregion Methods

    }

}

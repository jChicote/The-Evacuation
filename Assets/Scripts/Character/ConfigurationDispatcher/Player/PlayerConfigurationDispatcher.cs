using TheEvacuation.Model.Entities;
using TheEvacuation.PlayerSystems.Input;
using TheEvacuation.PlayerSystems.Movement;
using UnityEngine;

namespace TheEvacuation.Character.ConfigurationDispatcher.Player
{

    public class PlayerInputConfigurationPort
    {
        public SpaceShip SpaceShip { get; set; }
    }

    public class PlayerConfigurationDispatcher : MonoBehaviour, IConfigurationDispatcher<PlayerInputConfigurationPort>
    {
        #region - - - - - - Methods - - - - - -

        public void ConfigureGameObjectSystems(PlayerInputConfigurationPort inputPort)
        {
            InputControlledCharacter inputCharacter = this.GetComponent<InputControlledCharacter>();
            inputCharacter.InitiateInputSystem();

            IShipMovementSystem shipMovementSystem = GetComponent<IShipMovementSystem>();
            shipMovementSystem.InitialiseShipMovementSystem(inputPort.SpaceShip.shipAttributes);

            IInputToggling inputToggle = this.GetComponent<IInputToggling>();
            inputToggle.ToggleInputActivation(true);

            Destroy(this);
        }

        #endregion Methods

    }

}
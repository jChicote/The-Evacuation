using TheEvacuation.Infrastructure.GameSystems.SceneSystems;
using TheEvacuation.Interfaces.GameInterfaces.VitalityBars;
using TheEvacuation.Model.Entities;
using TheEvacuation.PlayerSystems.Input;
using TheEvacuation.PlayerSystems.Movement;
using TheEvacuation.PlayerSystems.Vitality;
using UnityEngine;

namespace TheEvacuation.Character.ConfigurationDispatcher.Player
{

    public class PlayerInputConfigurationPort
    {

        #region - - - - - - Properties - - - - - -

        public SpaceShip SpaceShip { get; set; }

        public IPlayerHealthBar HealthBar { get; set; }

        public IScenePauseEventHandler PauseEventHandler { get; set; }

        #endregion Properties

    }

    public class PlayerConfigurationDispatcher : MonoBehaviour, IConfigurationDispatcher<PlayerInputConfigurationPort>
    {

        #region - - - - - - Methods - - - - - -

        public void ConfigureGameObjectSystems(PlayerInputConfigurationPort inputPort)
        {
            PlayerInputSystemConfigurator inputCharacter = this.GetComponent<PlayerInputSystemConfigurator>();
            inputCharacter.InitiateInputSystem(inputPort.PauseEventHandler);

            IShipMovementSystem shipMovementSystem = GetComponent<IShipMovementSystem>();
            shipMovementSystem.InitialiseShipMovementSystem(inputPort.SpaceShip.shipAttributes);

            IPlayerHealthSystem healthSystem = this.GetComponent<IPlayerHealthSystem>();
            healthSystem.InitialisePlayerHealthSystem(inputPort.HealthBar, inputPort.SpaceShip.shipAttributes);

            IPlayerInputEnabler inputToggle = this.GetComponent<IPlayerInputEnabler>();
            inputToggle.EnableInputOperation(true);

            Destroy(this);
        }

        #endregion Methods

    }

}
using TheEvacuation.Common;
using TheEvacuation.Interfaces.GameInterfaces.VitalityBars;
using TheEvacuation.Model.Entities;
using UnityEngine;

namespace TheEvacuation.PlayerSystems.Vitality
{

    public class PlayerHealthSystem : MonoBehaviour, IDamageable, IPlayerHealthSystem
    {

        #region - - - - - - Fields - - - - - -

        public IPlayerHealthBar healthBar;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public float CurrentHealth { get; set; }
        public float MaxHealth { get; set; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public void InitialisePlayerHealthSystem(IPlayerHealthBar healthBar, ShipAttributes shipAttributes)
        {
            this.healthBar = healthBar;
            this.CurrentHealth = shipAttributes.maxSpeed;
            this.MaxHealth = shipAttributes.maxSpeed;

            healthBar.SetPlayerHealthBarValue(CurrentHealth, MaxHealth);
        }

        public void OnDamage(int damage)
        {
            CurrentHealth -= damage;

            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
                Debug.Log("Player is Dead");
            }

            healthBar.SetPlayerHealthBarValue(CurrentHealth, MaxHealth);
        }

        #endregion Methods

    }

}

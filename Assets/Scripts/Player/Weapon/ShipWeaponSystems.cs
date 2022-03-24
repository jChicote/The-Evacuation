using TheEvacuation.Common;
using UnityEngine;

namespace TheEvacuation.Player.Weapons
{
    public interface ICharacterWeaponSystem
    {
        bool IsFiring { get; set; }
    }

    public class ShipWeaponSystems : MonoBehaviour, ICharacterWeaponSystem
    {

        #region - - - - - - Fields - - - - - -

        protected IPausable pauseInstance;
        protected bool isFiring = false;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public bool IsFiring { get => isFiring; set => isFiring = value; }

        #endregion Properties

        #region - - - - - - MonoBehaviour - - - - - -

        void Start()
        {
            pauseInstance = this.GetComponent<IPausable>();
        }

        private void FixedUpdate()
        {
            FireWeapon();
        }

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - - -

        public virtual void FireWeapon()
        {
            if (!isFiring) return;

            print("is Firing");
        }

        #endregion Methods
    }
}

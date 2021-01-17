using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public interface IWeaponController
{
    void InitialiseWeaponController();
    void ActivateWeapons(bool isFiring);
}


namespace PlayerSystems
{
    /// <summary>
    /// Responsible for handling weapons fire and interfacing with weapons
    /// </summary>
    public class PlayerWeaponController : MonoBehaviour, IWeaponController
    {
        [Header("Weapon Loadout")]
        public LoadoutHolder[] forwardWeaponLoadout;
        public LoadoutHolder[] turrentWeaponLoadout;

        // Interfaces
        private ICheckPaused pauseChecker;
        private List<IWeapon> weapons;

        // Bool Checks
        private bool isFiring = false;

        /// <summary>
        /// Initialises the weapon controller.
        /// </summary>
        public void InitialiseWeaponController()
        {
            pauseChecker = this.GetComponent<ICheckPaused>();
            SetupWeapons();
        }

        /// <summary>
        /// Collects loadout positions and maps weapons to respective positions using the setup handler.
        /// </summary>
        private void SetupWeapons()
        {
            WeaponSetupHandler setuphandler = new WeaponSetupHandler();
            weapons = new List<IWeapon>();

            if (forwardWeaponLoadout.Length != 0)
            {
                setuphandler.SetupForwardWeapons(weapons, forwardWeaponLoadout);
            }

            if (turrentWeaponLoadout.Length != 0)
            {
                setuphandler.SetupTurrentWeapons(weapons, turrentWeaponLoadout);
            }
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (pauseChecker.CheckIsPaused()) return;
            if (!isFiring) return;

            FireWeapons();
        }

        /// <summary>
        /// Connects with the input system and activates the weapons on trigger.
        /// </summary>
        /// <param name="isFiring"></param>
        public void ActivateWeapons(bool isFiring)
        {
            this.isFiring = isFiring;
        }

        /// <summary>
        /// Fires the weapons.
        /// </summary>
        private void FireWeapons()
        {
            foreach (IWeapon weapon in weapons)
            {
                weapon.FireWeapon();
            }
        }
    }
}

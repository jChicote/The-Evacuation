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

            IShipData shipDataInterface = this.GetComponent<IShipData>();
            ShipStats shipStats = shipDataInterface.GetShipStats();

            SetupWeapons(shipStats);
        }

        /// <summary>
        /// Collects loadout positions and maps weapons to respective positions using the setup handler.
        /// </summary>
        private void SetupWeapons(ShipStats shipStats)
        {
            WeaponSetupHandler setuphandler = new WeaponSetupHandler();
            weapons = new List<IWeapon>();

            if (forwardWeaponLoadout.Length != 0 && shipStats.forwardLoadouts != null && shipStats.forwardLoadouts.Length != 0)
            {
                setuphandler.SetupForwardWeapons(weapons, shipStats.forwardLoadouts, forwardWeaponLoadout);
            }
            
            if (turrentWeaponLoadout.Length != 0 && shipStats.turrentLoadouts != null && shipStats.turrentLoadouts.Length != 0)
            {
                setuphandler.SetupTurrentWeapons(weapons, shipStats.turrentLoadouts, turrentWeaponLoadout);
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
            if (weapons.Count == 0)
            {
                Debug.Log("No Weapons Loaded");
                return;
            }

            foreach (IWeapon weapon in weapons)
            {
                weapon.FireWeapon();
            }
        }
    }
}

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
            ShipInfo shipInfo = shipDataInterface.GetShipStats();

            SetupWeapons(shipInfo);
        }

        /// <summary>
        /// Collects loadout positions and maps weapons to respective positions using the setup handler.
        /// </summary>
        private void SetupWeapons(ShipInfo shipInfo)
        {
            WeaponSetupHandler setuphandler = new WeaponSetupHandler();
            weapons = new List<IWeapon>();

            if (forwardWeaponLoadout.Length != 0 && shipInfo.forwardWeapons != null && shipInfo.forwardWeapons.Count != 0)
            {
                setuphandler.SetupForwardWeapons(weapons, shipInfo.forwardWeapons, forwardWeaponLoadout);
            }
            
            if (turrentWeaponLoadout.Length != 0 && shipInfo.turrentWeapons != null && shipInfo.turrentWeapons.Count != 0)
            {
                setuphandler.SetupTurrentWeapons(weapons, shipInfo.turrentWeapons, turrentWeaponLoadout);
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

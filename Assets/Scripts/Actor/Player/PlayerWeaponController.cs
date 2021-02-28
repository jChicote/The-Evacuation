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
    public interface IWeaponLoadoutSelector
    {
        void ChooseLoadoutPosition(LoadoutPosition positionType);
    }

    /// <summary>
    /// Responsible for handling weapons fire and interfacing with weapons
    /// </summary>
    public class PlayerWeaponController : MonoBehaviour, IWeaponController, IWeaponLoadoutSelector
    {
        [Header("Weapon Loadout")]
        [SerializeField] private LoadoutHolder[] forwardWeaponLoadout;
        [SerializeField] private LoadoutHolder[] turrentWeaponLoadout;

        // Interfaces
        private ICheckPaused pauseChecker;
        private List<IWeapon> weapons;

        // Weapon loadout firing configuration
        private LoadoutPosition loadoutPosition = LoadoutPosition.Forward;

        // Bool Checks
        private bool isFiring = false;

        /// <summary>
        /// Initialises the weapon controller.
        /// </summary>
        public void InitialiseWeaponController()
        {
            pauseChecker = this.GetComponent<ICheckPaused>();
            loadoutPosition = LoadoutPosition.Forward;

            IShipData shipDataInterface = this.GetComponent<IShipData>();
            ShipInfo shipInfo = shipDataInterface.GetShipStats();

            SetupWeapons(shipInfo);
        }

        private void FixedUpdate()
        {
            if (pauseChecker.CheckIsPaused()) return;
            if (!isFiring) return;

            FireWeapons();
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
                setuphandler.SetupForwardWeapons(ref weapons, shipInfo.forwardWeapons, forwardWeaponLoadout);
            }

            if (turrentWeaponLoadout.Length != 0 && shipInfo.turrentWeapons != null && shipInfo.turrentWeapons.Count != 0)
            {
                setuphandler.SetupTurrentWeapons(ref weapons, shipInfo.turrentWeapons, turrentWeaponLoadout);
            }
        }

        private void FireWeapons()
        {
            if (weapons.Count == 0)
            {
                Debug.Log("No Weapons Loaded");
                return;
            }

            foreach (IWeapon weapon in weapons)
            {
                weapon.FireWeapon(loadoutPosition);
            }
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
        /// Sets the loadout position on all weapons in ship
        /// </summary>
        /// <param name="positionType">Active position type either forward or pivot</param>
        public void ChooseLoadoutPosition(LoadoutPosition positionType)
        {
            loadoutPosition = positionType;
        }
    }
}

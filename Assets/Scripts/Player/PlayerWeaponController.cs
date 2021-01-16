using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponController
{
    void InitialiseWeaponController();
    void ActivateWeapons(bool isFiring);
} 

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


    private bool isFiring = false;

    public void InitialiseWeaponController()
    {
        pauseChecker = this.GetComponent<ICheckPaused>();
        SetupWeapons();
    }

    private void SetupWeapons()
    {
        weapons = new List<IWeapon>();

        if (forwardWeaponLoadout.Length != 0)
        {
            SetupForwardWeapons();
        }

        if (turrentWeaponLoadout.Length != 0)
        {
            SetupTurrentWeapons();
        }
    }

    //TODO: Change loadouts to exist within the stat handler and be a feature that is handled in the ship settings.
    //TODO: Consider moving setup methods into its own class.
    private void SetupForwardWeapons()
    {
        // TEMPORARY: ALL WEAPONS WILL BE SET TO DEFAULT BEFORE STAT HANDLER IMPLEMENTATION
        WeaponSettings settings = GameManager.Instance.weaponSettings;
        Loadout loadout = new Loadout();

        foreach (LoadoutHolder holder in forwardWeaponLoadout)
        {
            loadout.weaponInformation = settings.turrentWeapons[0];
            weapons.Add(holder.SetWeapon(loadout));
        }
    }

    private void SetupTurrentWeapons()
    {
        // TEMPORARY: ALL WEAPONS WILL BE SET TO DEFAULT BEFORE STAT HANDLER IMPLEMENTATION
        WeaponSettings settings = GameManager.Instance.weaponSettings;
        Loadout loadout = new Loadout();

        foreach (LoadoutHolder holder in turrentWeaponLoadout)
        {
            loadout.weaponInformation = settings.turrentWeapons[0];
            weapons.Add(holder.SetWeapon(loadout));
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (pauseChecker.CheckIsPaused()) return;
        if (!isFiring) return;

        FireWeapons();
        Debug.Log("Is Firing");
    }

    public void ActivateWeapons(bool isFiring)
    {
        this.isFiring = isFiring;
    }

    private void FireWeapons()
    {
        foreach (IWeapon weapon in weapons)
        {
            weapon.FireWeapon();
        }
    }
}

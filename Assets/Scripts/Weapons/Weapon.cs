using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void InitialiseWeapon(WeaponData data, GameObject projectileShell);
    void FireWeapon();
    void ConfigureWeaponPositioning(LoadoutPosition loadoutType);
}

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour, IWeapon, IPausable
    {
        //Weapon and Projectile data
        protected WeaponData weaponData;
        protected GameObject projectileShell;

        // Modifiable
        public SpriteRenderer renderer;
        public Transform firingPoint;
        protected bool isReloading = false;
        protected bool isPaused = false;
        protected float timeTillNextFire = 0;

        public abstract void InitialiseWeapon(WeaponData data, GameObject projectileShell);

        public abstract void FireWeapon();

        protected virtual void ReloadWeapon() { }

        public void ConfigureWeaponPositioning(LoadoutPosition loadoutType)
        {
            if (loadoutType == LoadoutPosition.Fixed)
            {
                renderer.enabled = false;
                firingPoint = transform;
            }  
        }

        public void OnPause()
        {
            isPaused = true;
        }

        public void OnUnpause()
        {
            isPaused = false;
        }
    }

}

[System.Serializable]
public class WeaponInfo
{
    public WeaponType weaponType;

    [Header("Stats")]
    public WeaponData weaponData;

    [Header("Prefab")]
    public GameObject weaponPrefab;
    public GameObject projectileShell;
}

[System.Serializable]
public struct WeaponData
{
    public float damage;
    public float speed;
    public float fireRate;
    public float life;
}

public enum WeaponType
{
    Turrent,
    Laser,
    Launcher
}


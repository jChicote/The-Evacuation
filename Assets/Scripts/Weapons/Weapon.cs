using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void InitialiseWeapon(WeaponData data);
    void FireWeapon();
    void ConfigureWeaponPositioning(LoadoutPosition loadoutType);
}

public interface IImageExtract
{

}

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour, IWeapon, IPausable
    {
        // Public Members
        public SpriteRenderer weaponRenderer;
        public Transform firingPoint;

        // Weapon Data
        protected WeaponData weaponData;

        // Modifiable
        protected bool isReloading = false;
        protected bool isPaused = false;
        protected float timeTillNextFire = 0;

        public abstract void InitialiseWeapon(WeaponData data);

        public abstract void FireWeapon();

        protected virtual void ReloadWeapon() { }

        /// <summary>
        /// Configures the weapons appearance and position based on loadout type.
        /// </summary>
        /// <param name="loadoutType"></param>
        public void ConfigureWeaponPositioning(LoadoutPosition loadoutType)
        {
            if (loadoutType == LoadoutPosition.Fixed)
            {
                weaponRenderer.enabled = false;
                firingPoint = transform;
            }  
        }

        /// <summary>
        /// Extracts image from renderer.
        /// </summary>
        /// <returns></returns>
        public Sprite ExtractImage()
        {
            return weaponRenderer.sprite;
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
public struct WeaponInfo
{
    public string stringID;

    [Header("Data")]
    public WeaponData weaponData;

    [Header("Prefabs")]
    public GameObject weaponPrefab;

}

[System.Serializable]
public struct WeaponData
{
    public string stringID;
    public WeaponType weaponType;

    [Header("Data Members")]
    public float damage;
    public float speed;
    public float fireRate;
    public float life;
    public int spread;

    [Header("Additional Data")]
    public float price;
}

public enum WeaponType
{
    Turrent,
    Laser,
    Launcher
}


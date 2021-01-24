using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void InitialiseWeapon(WeaponInfo data);
    void FireWeapon();
    void ConfigureWeaponPositioning(LoadoutPosition loadoutType);
}

public interface IImageExtract
{
    Sprite ExtractImage();
}

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour, IWeapon, IPausable, IImageExtract
    {
        // Public Members
        public SpriteRenderer weaponRenderer;
        public Transform firingPoint;

        // Weapon Data
        protected WeaponInfo weaponData;

        // Modifiable
        protected bool isReloading = false;
        protected bool isPaused = false;
        protected float timeTillNextFire = 0;

        public abstract void InitialiseWeapon(WeaponInfo data);

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
public class ObjectInfo
{
    public string stringID;
    public string uniqueID; //This is an id that is uniquly assigned to this object - BUT WILL BE IGNORED FOR NOW
    public string name;
    public float price;
}
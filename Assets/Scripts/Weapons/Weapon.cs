using UnityEngine;
using Evacuation.Actor;

public interface IWeapon
{
    void InitialiseWeapon(WeaponInfo data, IEntitySpeed movementAccessors);
    void FireWeapon();
    void ConfigureWeaponPositioning(LoadoutConfiguration loadoutType);
    bool CheckIfValidLoadoutPosition(LoadoutConfiguration loadoutType);
}

public interface IImageExtract
{
    Sprite ExtractImage();
}

namespace Evacuation.Weapons
{

    public interface IWeaponRotator
    {
        void ProvidePointerLocation(Vector2 pointerPosition);
        void RotateWeaponToDirection();
    }

    public abstract class Weapon : MonoBehaviour, IWeapon, IPausable, IImageExtract, IWeaponRotator
    {
        // Inspector Accesible Members
        [SerializeField] protected SpriteRenderer weaponRenderer;
        [SerializeField] protected Transform firingPoint;

        // Fields
        protected Transform weaponTransform;
        protected WeaponInfo weaponData;
        protected LoadoutConfiguration loadoutConfiguration;
        protected Vector3 relativeWeaponDirectionToPoint;
        protected Vector3 lastPointedPosition;
        protected bool isReloading = false;
        protected bool isPaused = false;
        protected float timeTillNextFire = 0;
        protected float pointedAngle = 0;

        public abstract void InitialiseWeapon(WeaponInfo data, IEntitySpeed movementAccessors);

        public abstract void FireWeapon();

        protected virtual void ReloadWeapon() { }

        public virtual void RotateWeaponToDirection()
        {
            relativeWeaponDirectionToPoint = lastPointedPosition - transform.position;
            pointedAngle = Mathf.Atan2(relativeWeaponDirectionToPoint.y, relativeWeaponDirectionToPoint.x) * Mathf.Rad2Deg - 90;
            weaponTransform.rotation = Quaternion.Euler(0, 0, pointedAngle);
        }

        public virtual void ProvidePointerLocation(Vector2 pointerPosition) { }

        /// <summary>
        /// Configures the weapons appearance and position based on loadout type.
        /// </summary>
        /// <param name="loadoutType"></param>
        public void ConfigureWeaponPositioning(LoadoutConfiguration configuration)
        {
            if (configuration == LoadoutConfiguration.Forward)
            {
                weaponRenderer.enabled = false;
                firingPoint = transform;
            }

            this.loadoutConfiguration = configuration;
        }

        /// <summary>
        /// Extracts image from renderer.
        /// </summary>
        /// <returns></returns>
        public Sprite ExtractImage()
        {
            return weaponRenderer.sprite;
        }

        public bool CheckIfValidLoadoutPosition(LoadoutConfiguration configuration)
        {
            return loadoutConfiguration == configuration;
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
    public string name;
    public string stringID;
    public int price;

    [HideInInspector] 
    public EquipmentType equipmentType;
}
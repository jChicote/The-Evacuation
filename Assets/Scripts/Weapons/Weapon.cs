using UnityEngine;
using PlayerSystems;

public interface IWeapon
{
    void InitialiseWeapon(WeaponInfo data, IMovementAccessors movementAccessors);
    void FireWeapon(LoadoutPosition currentLoadoutPosition);
    void ConfigureWeaponPositioning(LoadoutPosition loadoutType);
}

public interface IImageExtract
{
    Sprite ExtractImage();
}

namespace Weapons
{

    public interface IWeaponRotator
    {
        void ProvidePointerLocation(Vector2 pointerPosition);
        void RotateWeaponToPosition(LoadoutPosition currentLoadoutPosition);
    }

    public abstract class Weapon : MonoBehaviour, IWeapon, IPausable, IImageExtract, IWeaponRotator
    {
        // Public Members
        [SerializeField] protected SpriteRenderer weaponRenderer;
        [SerializeField] protected Transform firingPoint;

        // Fields
        protected Transform weaponTransform;
        protected WeaponInfo weaponData;
        protected LoadoutPosition loadoutPositionType;
        protected Vector3 relativeWeaponDirectionToPoint;
        protected Vector3 lastPointedPosition;
        protected bool isReloading = false;
        protected bool isPaused = false;
        protected float timeTillNextFire = 0;
        protected float pointedAngle = 0;

        public abstract void InitialiseWeapon(WeaponInfo data, IMovementAccessors movementAccessors);

        public abstract void FireWeapon(LoadoutPosition currentLoadoutPosition);

        protected virtual void ReloadWeapon() { }

        public virtual void RotateWeaponToPosition(LoadoutPosition currentLoadoutPosition)
        {
            if (loadoutPositionType != currentLoadoutPosition) return;

            relativeWeaponDirectionToPoint = lastPointedPosition - transform.position;
            pointedAngle = Mathf.Atan2(relativeWeaponDirectionToPoint.y, relativeWeaponDirectionToPoint.x) * Mathf.Rad2Deg - 90;
            weaponTransform.rotation = Quaternion.Euler(0, 0, pointedAngle);
        }

        public virtual void ProvidePointerLocation(Vector2 pointerPosition) { }

        /// <summary>
        /// Configures the weapons appearance and position based on loadout type.
        /// </summary>
        /// <param name="loadoutType"></param>
        public void ConfigureWeaponPositioning(LoadoutPosition loadoutType)
        {
            if (loadoutType == LoadoutPosition.Forward)
            {
                weaponRenderer.enabled = false;
                firingPoint = transform;
            }

            this.loadoutPositionType = loadoutType;
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
    //public string uniqueID; //This is an id that is uniquly assigned to this object - BUT WILL BE IGNORED FOR NOW
    public string name;
    public int price;

    [HideInInspector] 
    public EquipmentType equipmentType;
}
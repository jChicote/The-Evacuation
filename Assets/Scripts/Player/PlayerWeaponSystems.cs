using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheEvacuation.Character.Weapons;
using TheEvacuation.Weapon;

namespace TheEvacuation.Player.Weapons
{
    public interface IInputWeaponSystemVariables
    {
        Vector2 InputMousePosition { get; set; }
    }

    public interface IWeaponGroupModifier
    {
        void ChangeActiveWeaponGroup(ActiveWeaponGroup current);
    }

    public class PlayerWeaponSystems : CharacterWeaponSystem, IInputWeaponSystemVariables, IWeaponGroupModifier
    {
        // Field
        [SerializeField] protected GameObject[] fixedWeapons;
        [SerializeField] protected GameObject[] revolvingWeapons;
        private ActiveWeaponGroup weaponGroup = ActiveWeaponGroup.Fixed;
        private Vector2 inputMousePosition = Vector2.zero;

        protected IWeapon[] fixedWeaponInterfaces;
        protected IWeapon[] turrentWeaponInterfaces;

        protected IWeapon[] playerWeapons;
        protected IWeaponRotator[] rotatingWeapons;
        
        // Properties
        public Vector2 InputMousePosition { get => inputMousePosition; set => inputMousePosition = value; }

        private void Start()
        {
            pauseInstance = this.GetComponent<IPausable>();

            fixedWeaponInterfaces = new IWeapon[1];
            fixedWeaponInterfaces[0] = fixedWeapons[0].GetComponent<IWeapon>();

            playerWeapons = this.GetComponentsInChildren<IWeapon>();
            rotatingWeapons = this.GetComponentsInChildren<IWeaponRotator>();
        }

        private void FixedUpdate()
        {
            if (!isFiring) return;
            FireWeapon();
        }
        
        public override void FireWeapon()
        {
            for(int i = 0; i < playerWeapons.Length; i++)
            {
                fixedWeaponInterfaces[i].Fire();
                fixedWeaponInterfaces[i].MousePosition = inputMousePosition;
            }
        }

        public void ChangeActiveWeaponGroup(ActiveWeaponGroup current)
        {
            weaponGroup = current;
            
            for (int i = 0; i < rotatingWeapons.Length; i++)
            {
                rotatingWeapons[i].CanRotate = weaponGroup == ActiveWeaponGroup.Revolving;
            }
        }
    }

    public enum ActiveWeaponGroup
    {
        Fixed,
        Revolving
    }
}

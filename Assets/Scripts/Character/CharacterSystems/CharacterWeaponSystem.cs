using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheEvacuation.Character.Weapons
{
    public interface ICharacterWeaponController
    {
        bool IsFiring { get; set; }
    }

    public class CharacterWeaponSystem : MonoBehaviour, ICharacterWeaponController
    {
        // Fields
        protected IPausable pauseInstance;
        protected bool isFiring = false;

        // Accessors
        public bool IsFiring { get => isFiring; set => isFiring = value; }

        // Start is called before the first frame update
        void Start()
        {
            pauseInstance = this.GetComponent<IPausable>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            FireWeapon();
        }

        public virtual void FireWeapon()
        {
            if (!isFiring) return;

            print("is Firing");
        }
    }
}

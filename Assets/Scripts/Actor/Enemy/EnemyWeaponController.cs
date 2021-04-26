using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Actor.EnemySystems
{
    public interface IWeaponController
    {
        void InitialiseWeaponController();
    }

    public class EnemyWeaponController : MonoBehaviour, IWeaponController
    {
        // Fields
        protected IWeapon[] collectedWeapons;

        public void InitialiseWeaponController()
        {

        }

        // Update is called once per frame
        private void FixedUpdate()
        {

        }


        private void CollectAllWeapons()
        {

        }
    }
}

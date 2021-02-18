using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public interface IWeaponButton
{
    bool CheckIsHolding();
}

namespace UserInterface.Touch
{
    public class TouchTriggerControl : MonoBehaviour
    {

        private IWeaponButton weaponButton;

        // Start is called before the first frame update
        void Start()
        {
            weaponButton = this.GetComponentInChildren<IWeaponButton>();
        }

        private void FixedUpdate()
        {
            if(weaponButton.CheckIsHolding())
            {
                ExecuteWeaponEvent();
            }
        }

        /// <summary>
        /// Calls interface event for weapon fire.
        /// </summary>
        private void ExecuteWeaponEvent()
        {
            Debug.Log("Execute weapon event");
        }

        /// <summary>
        /// Calls attached interface for powerup.
        /// </summary>
        public void ExecutePowerupEvent()
        {
            Debug.Log("Execute powerup event");
        }

        /// <summary>
        /// Calls attached interface for boost thrust.
        /// </summary>
        public void ExecuteBoostEvent()
        {
            Debug.Log("Execute boost event");
        }
    }
}

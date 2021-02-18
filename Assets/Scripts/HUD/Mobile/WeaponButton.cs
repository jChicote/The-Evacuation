using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterface.Touch
{
    public class WeaponButton : PressAndHoldEvent, IWeaponButton
    {
        public bool CheckIsHolding()
        {
            return isHold;
        }
    }
}
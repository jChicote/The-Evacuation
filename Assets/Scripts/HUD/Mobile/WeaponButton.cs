using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterfaces.Touch
{
    public class WeaponButton : PressAndHoldEvent, IWeaponButton
    {
        public bool CheckIsHolding()
        {
            return isHold;
        }
    }
}
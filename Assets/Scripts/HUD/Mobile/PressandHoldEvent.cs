using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UserInterface.Touch
{
    public class PressAndHoldEvent : Button
    {
        protected bool isHold = false;

        /// <summary>
        /// Override button for custom functionality when the input is down.
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            isHold = true;
        }

        /// <summary>
        /// Override button for custom functionality whem the input is up.
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            isHold = false;
        }
    }
}

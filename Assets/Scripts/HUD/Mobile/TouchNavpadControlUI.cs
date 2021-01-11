using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ITouchLeftNavpadControl
{
    void RevealPad(Vector2 begintouchPosition);
    void TransformNavStick(Vector2 touchPosition);
    void HidePad();
}

namespace UserInterfaces.Touch
{
    public class TouchNavpadControlUI : MonoBehaviour, ITouchLeftNavpadControl
    {
        [Header("Navigation Control Pad")]
        public GameObject touchNavElement;
        public RectTransform touchStickIndicator;
        public RectTransform touchCircle;

        [Header("Navpad Images")]
        public Image indicatorImage;
        public Image circleImage;

        // Transform related variables
        private Vector2 centerPosition;
        private float maxRadiusTransform; //Clamp distance
        private float distanceToCenter;

        // Start is called before the first frame update
        void Start()
        {

        }

        /// <summary>
        /// Hides pad when exiting touch
        /// </summary>
        public void HidePad()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Reveals the pad when entering touch
        /// </summary>
        public void RevealPad(Vector2 begintouchPosition)
        {
            centerPosition = begintouchPosition;
        }

        /// <summary>
        /// Transforms the position of the indicator relative from the intiial touch position
        /// </summary>
        /// <param name="touchPosition"></param>
        public void TransformNavStick(Vector2 touchPosition)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Fades Navpad to appear after detecting touch.
        /// </summary>
        /// <returns></returns>
        private IEnumerator FadeInNavpadUI()
        {
            float alpha = indicatorImage.color.a;

            yield return null;
        }

        /// <summary>
        /// Fades out navpad on touch exit.
        /// </summary>
        /// <returns></returns>
        private IEnumerator FadeOutNavpadUI()
        {
            float alpha = indicatorImage.color.a;

            yield return null;
        }
    }
}

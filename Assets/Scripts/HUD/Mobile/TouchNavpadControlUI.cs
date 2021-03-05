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

namespace Evacuation.UserInterface.Touch
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
        private Vector2 currentTouchPosition; //TODO: NEED TO REMOVE
        private Vector2 centerPosition;
        private float maxRadiusTransform = 450f / 2; // clamp distance and contains FEAULT VALUES ATM
        private float distanceToCenter;

        /// <summary>
        /// Hides pad when exiting touch
        /// </summary>
        public void HidePad()
        {
            if (!touchNavElement.activeInHierarchy) return;

            StopCoroutine("FadeInNavpadUI");
            StartCoroutine("FadeOutNavpadUI");
        }

        /// <summary>
        /// Reveals the pad when entering touch
        /// </summary>
        public void RevealPad(Vector2 begintouchPosition)
        {
            if (touchNavElement.activeInHierarchy) return;

            centerPosition = begintouchPosition;
            touchStickIndicator.position = centerPosition;
            touchCircle.position = centerPosition;

            StopCoroutine("FadeOutNavpadUI");
            StartCoroutine("FadeInNavpadUI");
        }

        /// <summary>
        /// Transforms the position of the indicator relative from the intiial touch position
        /// </summary>
        /// <param name="touchPosition"></param>
        public void TransformNavStick(Vector2 touchPosition)
        {
            distanceToCenter = Vector3.Magnitude(centerPosition - touchPosition);
            distanceToCenter = Mathf.Clamp(distanceToCenter, 0, maxRadiusTransform);
            currentTouchPosition = (touchPosition - centerPosition).normalized * distanceToCenter;

            touchStickIndicator.position = currentTouchPosition + centerPosition;
        }

        /// <summary>
        /// Fades Navpad to appear after detecting touch.
        /// </summary>
        /// <returns></returns>
        private IEnumerator FadeInNavpadUI()
        {
            float alpha = indicatorImage.color.a;
            float deltaVal = 5 * Time.deltaTime;
            Color tempColor = Color.white;

            tempColor.a = alpha;
            touchNavElement.SetActive(true);

            while (alpha < 1)
            {
                alpha += deltaVal; // modifies alpha value
                tempColor.a = alpha;
                indicatorImage.color = tempColor;
                circleImage.color = tempColor;
                yield return null;
            }
        }

        /// <summary>
        /// Fades out navpad on touch exit.
        /// </summary>
        /// <returns></returns>
        private IEnumerator FadeOutNavpadUI()
        {
            float alpha = indicatorImage.color.a;
            float deltaVal = 5 * Time.deltaTime;
            Color tempColor = Color.white;

            tempColor.a = alpha;

            while (alpha < 1)
            {
                alpha -= deltaVal; // modifies alpha value
                tempColor.a = alpha;
                indicatorImage.color = tempColor;
                circleImage.color = tempColor;
                yield return null;
            }

            touchNavElement.SetActive(false);
        }
    }
}

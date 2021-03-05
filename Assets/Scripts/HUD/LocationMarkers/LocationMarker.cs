using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Evacuation.UserInterface.LocationMarker
{
    public interface ILocationMarker
    {
        void InitialiseMarker(Transform locationTransform, RectTransform parentCanvasTransform);
        void SetMarkerConstraints(IMarkerConstraints markerConstraints);
        void RunMarker();
    }

    public class LocationMarker : MonoBehaviour, ILocationMarker, IPausable
    {
        // Interfaces
        private IMarkerConstraints markerConstraints;

        // Inspector Accessible Fields
        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Sprite inactiveSprite;

        // Fields
        private Transform locationTargetTransform;
        private RectTransform parentCanvasTransform;
        private RectTransform markerRectTransform;
        private Vector2 markerWorldPosition = Vector2.zero;
        private Vector2 scaledScreenPosition = Vector2.zero;
        private Image markerImage;
        private bool isPaused = false;

        public void InitialiseMarker(Transform locationTransform, RectTransform parentCanvasTransform)
        {
            locationTargetTransform = locationTransform;
            this.parentCanvasTransform = parentCanvasTransform;
            this.markerRectTransform = this.GetComponent<RectTransform>();
            markerImage = this.GetComponent<Image>();
        }

        private bool CheckWithinConstraint()
        {
            Debug.Log(locationTargetTransform.position);

            return locationTargetTransform.position.x < markerConstraints.RightConstraint 
                && locationTargetTransform.position.x > markerConstraints.LeftConstraint 
                && locationTargetTransform.position.y < markerConstraints.TopConstraint 
                && locationTargetTransform.position.y > markerConstraints.BottomConstraint;
        }

        private void DetermineWorldPosition()
        {
            markerWorldPosition = locationTargetTransform.position;

            //if (markerWorldPosition.x > markerConstraints.RightConstraint) markerWorldPosition.x = markerConstraints.RightConstraint;
            //if (markerWorldPosition.x < markerConstraints.LeftConstraint) markerWorldPosition.x = markerConstraints.LeftConstraint;
            //if (markerWorldPosition.y > markerConstraints.TopConstraint) markerWorldPosition.y = markerConstraints.TopConstraint;
            //if (markerWorldPosition.y < markerConstraints.BottomConstraint) markerWorldPosition.y = markerConstraints.BottomConstraint;

            scaledScreenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, scaledScreenPosition);
            scaledScreenPosition.x = parentCanvasTransform.rect.width * (scaledScreenPosition.x / Screen.width);
            scaledScreenPosition.y = parentCanvasTransform.rect.height * (scaledScreenPosition.y / Screen.height);
        }

        private void RenderMarkerPointer()
        {
            markerImage.enabled = true;

            DetermineWorldPosition();
            markerRectTransform.anchoredPosition = scaledScreenPosition;
            Debug.Log("marker position = " + parentCanvasTransform.anchoredPosition);
        }

        public void SetMarkerConstraints(IMarkerConstraints markerConstraints)
        {
            this.markerConstraints = markerConstraints;
        }

        public void RunMarker()
        {
            if (isPaused) return;
            Debug.Log(markerWorldPosition.x + " , " + markerConstraints.RightConstraint);
            RenderMarkerPointer();

            /*if (!CheckWithinConstraint())
            {
                RenderMarkerPointer();
            }
            else
            {
                markerImage.enabled = false;
            }*/
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
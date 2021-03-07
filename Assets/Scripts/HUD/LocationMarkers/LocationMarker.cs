using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Evacuation.UserInterface.LocationMarker
{
    public interface ILocationMarker
    {
        void InitialiseMarker(Transform locationTransform, RectTransform parentCanvasTransform);
        void SetMarkerConstraints(IMarkerConstraints markerConstraints);
        void SetMarkerIconType(MarkerType markerType);
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
            return locationTargetTransform.position.x < markerConstraints.RightConstraint 
                && locationTargetTransform.position.x > markerConstraints.LeftConstraint 
                && locationTargetTransform.position.y < markerConstraints.TopConstraint 
                && locationTargetTransform.position.y > markerConstraints.BottomConstraint;
        }

        private void DetermineWorldPosition()
        {
            markerWorldPosition = locationTargetTransform.position;

            scaledScreenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, markerWorldPosition);

            if (scaledScreenPosition.x > markerConstraints.RightConstraint) scaledScreenPosition.x = markerConstraints.RightConstraint;
            if (scaledScreenPosition.x < markerConstraints.LeftConstraint) scaledScreenPosition.x = markerConstraints.LeftConstraint;
            if (scaledScreenPosition.y > markerConstraints.TopConstraint) scaledScreenPosition.y = markerConstraints.TopConstraint;
            if (scaledScreenPosition.y < markerConstraints.BottomConstraint) scaledScreenPosition.y = markerConstraints.BottomConstraint;

            scaledScreenPosition.x = parentCanvasTransform.rect.width * (scaledScreenPosition.x / Screen.width);
            scaledScreenPosition.y = parentCanvasTransform.rect.height * (scaledScreenPosition.y / Screen.height);
        }

        private void RenderMarkerPointer()
        {
            markerImage.enabled = true;

            DetermineWorldPosition();
            markerRectTransform.anchoredPosition = scaledScreenPosition;
        }

        public void SetMarkerConstraints(IMarkerConstraints markerConstraints)
        {
            this.markerConstraints = markerConstraints;
        }

        public void RunMarker()
        {
            if (isPaused) return;

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

        public void SetMarkerIconType(MarkerType markerType)
        {
            UISettings uiSettings = GameManager.Instance.uiSettings;
            activeSprite = uiSettings.markerTypes.Where(x => x.type == markerType).First().sprite;
            inactiveSprite = uiSettings.markerTypes.Where(x => x.type == MarkerType.Inactive).First().sprite;
            markerImage.sprite = activeSprite;
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

    public enum MarkerType
    {
        Island,
        RescueShip,
        Enemy,
        Inactive
    }
}
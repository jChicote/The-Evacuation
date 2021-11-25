using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.UserInterface.LocationMarker
{
    public interface IMarkerConstraints
    {
        float TopConstraint { get; }
        float BottomConstraint { get; }
        float RightConstraint { get; }
        float LeftConstraint { get;  }
    }

    public interface IMarkerManager
    {
        void InitialiseMarkerManager();
        void GenerateLocationMarker(Transform platformTransform, MarkerType markerType);
    }

    public class LocationMarkerManager : MonoBehaviour, IPausable, IMarkerConstraints, IMarkerManager
    {
        // Constants
        private float screenWidth;
        private float screenHeight;

        // Fields
        private Transform cameraTransform;
        private RectTransform rectTransform;
        private ILocationMarker[] locationMarkers;
        private bool isPaused = false;
        private float rightConstraint;
        private float leftConstraint;
        private float bottomConstraint;
        private float topConstraint;

        // Accessors
        public float TopConstraint { get { return topConstraint; } }
        public float BottomConstraint { get { return bottomConstraint; } }
        public float RightConstraint { get { return rightConstraint; } }
        public float LeftConstraint { get { return leftConstraint; } }

        public void InitialiseMarkerManager()
        {
            this.cameraTransform = Camera.main.transform;
            this.rectTransform = this.GetComponent<RectTransform>();

            //screenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 1, 0)).x;
            //screenHeight = Camera.main.ScreenToWorldPoint(new Vector3(1, Screen.height, 0)).y;

            screenWidth = Screen.width;
            screenHeight = Screen.height;

            SetScreenConstraints();
        }

        private void SetScreenConstraints()
        {
            // TODO: adjust for dynamic resolution
            rightConstraint = screenWidth - 80;
            leftConstraint = 50;
            topConstraint =  screenHeight - 80;
            bottomConstraint = 50;
        }

        private void FixedUpdate()
        {
            if (isPaused) return;

            UpdateMarkerCollection();
        }

        private void UpdateMarkerCollection()
        {
            if (locationMarkers == null) return;

            for (int i = 0; i < locationMarkers.Length; i++)
            {
                locationMarkers[i].RunMarker();
            }
        }

        private void UpdateArrayCollection(List<ILocationMarker> tempArray)
        {
            locationMarkers = new ILocationMarker[tempArray.Count];

            for (int i = 0; i < tempArray.Count; i++)
                locationMarkers[i] = tempArray[i];
        }

        private void CopyAllItemsFromArray(List<ILocationMarker> tempArray)
        {
            if (locationMarkers == null) return;

            for (int i = 0; i < locationMarkers.Length; i++)
                tempArray.Add(locationMarkers[i]);
        }

        public void GenerateLocationMarker(Transform platformTransform, MarkerType markerType)
        {
            UISettings settings = GameManager.Instance.uiSettings;
            GameObject generatedInstance = Instantiate(settings.markerPrefab, this.transform);
            ILocationMarker markerInterface = generatedInstance.GetComponent<ILocationMarker>();
            markerInterface.InitialiseMarker(platformTransform, rectTransform);
            markerInterface.SetMarkerConstraints(this);
            markerInterface.SetMarkerIconType(markerType);

            List<ILocationMarker> tempArray = new List<ILocationMarker>();
            CopyAllItemsFromArray(tempArray);
            tempArray.Add(markerInterface);
            UpdateArrayCollection(tempArray);
        }

        public void DeleteLocationMarker()
        {

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

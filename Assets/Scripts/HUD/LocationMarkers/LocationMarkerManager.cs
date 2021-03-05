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
        void GenerateLocationMarker(Transform platformTransform);
    }

    public class LocationMarkerManager : MonoBehaviour, IPausable, IMarkerConstraints, IMarkerManager
    {
        public GameObject markerPrefab;

        // Constants
        private readonly float screenWidth = Screen.width;
        private readonly float screenHeight = Screen.height;

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
        }

        private void SetScreenConstraints()
        {
            // TODO: adjust for dynamic resolution
            rightConstraint = cameraTransform.position.x + ((screenWidth / 2) - 5);
            leftConstraint = cameraTransform.position.x - ((screenWidth / 2) + 5);
            topConstraint = cameraTransform.position.y + ((screenHeight / 2) - 2);
            bottomConstraint = cameraTransform.position.y - ((screenHeight / 2) + 2);
        }

        private void FixedUpdate()
        {
            if (isPaused) return;

            SetScreenConstraints();
            UpdateMarkerCollection();
        }

        private void UpdateMarkerCollection()
        {
            for (int i = 0; i < locationMarkers.Length; i++)
            {
                locationMarkers[i].RunMarker();
            }
        }

        private void UpdateArrayCollection(List<ILocationMarker> tempArray)
        {
            locationMarkers = new ILocationMarker[tempArray.Count];

            for (int i = 0; i < tempArray.Count; i++)
            {
                locationMarkers[i] = tempArray[i];
            }
        }

        public void GenerateLocationMarker(Transform platformTransform)
        {
            Debug.Log(" Marker was generated ");

            GameObject generatedInstance = Instantiate(markerPrefab, this.transform);
            ILocationMarker markerInterface = generatedInstance.GetComponent<ILocationMarker>();
            markerInterface.InitialiseMarker(platformTransform, rectTransform);
            markerInterface.SetMarkerConstraints(this);

            List<ILocationMarker> tempArray = new List<ILocationMarker>();

            for (int i = 0; i < tempArray.Count; i++)
                tempArray.Add(locationMarkers[i]);


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

    public enum MarkerType
    {
        Island,
        RescueShip,
        Enemy
    }
}

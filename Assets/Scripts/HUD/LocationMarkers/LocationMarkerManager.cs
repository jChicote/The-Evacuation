using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterface.LocationMarker
{

    public class LocationMarkerManager : MonoBehaviour, IPausable
    {
        private ILocationMarker[] locationMarkers;
        private bool isPaused = false;

        public GameObject markerPrefab;

        private void FixedUpdate()
        {
            if (isPaused) return;

            UpdateMarkerCollection();
        }

        private void UpdateMarkerCollection()
        {
            for (int i = 0; i < locationMarkers.Length; i++)
            {
                locationMarkers[i].RunMarker();
            }
        }

        public void GenerateLocationMarker(Transform platformTransform)
        {

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

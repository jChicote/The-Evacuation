using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Evacuation.Cinematics.CameraUtil;

namespace Evacuation.Cinematics
{
    public class CinematicManager : MonoBehaviour
    {
        // Fields
        private CinemachineVirtualCamera primaryVirtualCamera;
        private ICameraZoom cameraZoom;

        // Properties
        public CinemachineVirtualCamera PrimaryCamera 
        { 
            get => primaryVirtualCamera;
            set
            {
                primaryVirtualCamera = value;
                cameraZoom = primaryVirtualCamera.GetComponent<ICameraZoom>();
            }
        }

        public ICameraZoom CameraZoom { get => cameraZoom; }
    }
}

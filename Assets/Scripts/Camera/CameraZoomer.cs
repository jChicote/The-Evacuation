using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Evacuation.Cinematics.CameraUtil
{

    public interface ICameraZoom
    {
        void SetTargetZoom(float zoomValue);
        void SetToDefaultZoom();
    }

    public class CameraZoomer : MonoBehaviour, IPausable, ICameraZoom
    {
        // Inspector Accesisble Fields
        [SerializeField] private float defaultZoom;
        [Range(0, 1)]
        [SerializeField] private float interpolationStep;

        // Fields
        private CinemachineVirtualCamera mainCamera;
        private float targetZoom;
        private float currentZoom;
        private float timeStep = 0;
        private bool isPaused = false;
        public bool isAtTargetZoom = true;

        private void Awake()
        {
            currentZoom = defaultZoom;
            mainCamera = this.GetComponent<CinemachineVirtualCamera>();
            defaultZoom = mainCamera.m_Lens.OrthographicSize;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (isPaused || isAtTargetZoom) return;
            LerpToTargetZoom();
        }

        private void LerpToTargetZoom()
        {
            currentZoom = Mathf.Lerp(currentZoom, targetZoom, interpolationStep);
            timeStep += interpolationStep * Time.deltaTime;
            mainCamera.m_Lens.OrthographicSize = currentZoom;

            if (System.Math.Round(currentZoom, 1) >= targetZoom - 0.1f) isAtTargetZoom = true;
        }

        public void SetTargetZoom(float zoomValue)
        {
            targetZoom = zoomValue;
            timeStep = 0;
            //print("targetz zoom at: " + zoomValue);
            isAtTargetZoom = false;
        }

        public void SetToDefaultZoom()
        {
            targetZoom = defaultZoom;
            timeStep = 0;
            //print("Default zoom at: " + defaultZoom);
            isAtTargetZoom = false;
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

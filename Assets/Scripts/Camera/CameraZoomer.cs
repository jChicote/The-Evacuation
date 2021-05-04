using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Evacuation.CameraUtil
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
        [SerializeField] private float interpolateDuration;

        // Fields
        private CinemachineVirtualCamera mainCamera;
        private float targetZoom;
        private float currentZoom;
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
            currentZoom = Mathf.Lerp(currentZoom, defaultZoom, interpolateDuration);
            mainCamera.m_Lens.OrthographicSize = currentZoom;
            //print(currentZoom);

            if (currentZoom == targetZoom) isAtTargetZoom = true;
        }

        public void SetTargetZoom(float zoomValue)
        {
            targetZoom = zoomValue;
            isAtTargetZoom = false;
        }

        public void SetToDefaultZoom()
        {
            targetZoom = defaultZoom;
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

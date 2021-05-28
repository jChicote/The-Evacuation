using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Actor.PlayerSystems;

namespace Evacuation.Level.TransportSystems
{
    public interface ICaptureRenderer
    {
        void BeginRenderingCaptureVisual(IAutoLandingSystem shipPositionLocator);
        void StopRenderingCaptureVisual();
        void ShrinkCaptureCircle();
        void PositionCaptureLine();
    }

    public class CaptureRenderer : MonoBehaviour, ICaptureRenderer
    {
        // Interfaces
        private IShipPositionLocator shipPositionLocator;

        // Inspector Accessible Fields
        [SerializeField] private Transform captureCircle;
        [SerializeField] private LineRenderer captureLine;

        // Fields
        private float scaleValue;

        public void BeginRenderingCaptureVisual(IAutoLandingSystem landingSystem)
        {
            this.shipPositionLocator = landingSystem;

            // Initialise visual position
            ShrinkCaptureCircle();
            PositionCaptureLine();

            captureCircle.gameObject.SetActive(true);
            captureLine.gameObject.SetActive(true);
        }

        public void StopRenderingCaptureVisual()
        {
            captureCircle.gameObject.SetActive(false);
            captureCircle.localScale = new Vector2(1, 1);
            scaleValue = 1;

            captureLine.gameObject.SetActive(false);
            shipPositionLocator = null;
        }

        public void ShrinkCaptureCircle()
        {
            scaleValue = Mathf.Lerp(captureCircle.localScale.x, 0, Time.fixedDeltaTime);
            captureCircle.localScale = new Vector2(scaleValue, scaleValue);
        }

        public void PositionCaptureLine()
        {
            captureLine.SetPosition(0, new Vector2(0, 0));
            captureLine.SetPosition(1, transform.InverseTransformPoint(shipPositionLocator.GetShipPosition()));
        }

    }
}

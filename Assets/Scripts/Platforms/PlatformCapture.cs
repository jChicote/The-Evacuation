using System;
using UnityEngine;
using PlayerSystems;

namespace Level.TransportSystems
{
    // Summary:
    //      This interfaces defines the abstracted calls externally for this class.
    public interface ICapture
    {
        void EndCapture();
        void InitialiseCaptureSystem(ITransportPlatform transporterPlatform);
    }

    // Summary:
    //      This class is used for capturing running guiding calls to draw player ship into its destination.
    //      This is utilised similarly for both drop off and pickup of island inhabitants.
    public class PlatformCapture : MonoBehaviour, IPausable, ICapture
    {
        // Interface variables
        public ITransportPlatform platform;
        public IAutoLandingManoeuvre shipLandingManoeuvre;
        public ICaptureRenderer captureRenderer;
        public IShipPlatformTranslator shipTranslate;
        public IShipPositionLocator shipPositionLocator;

        private CaptureState captureState = null;

        // Inspector Accessible Fields
        [SerializeField] private Timer timer;

        // Fields
        private bool isPaused = false;

        // Accessors
        public Timer CaptureTimer
        {
           get { return timer;  }
        }

        public void InitialiseCaptureSystem(ITransportPlatform transporterPlatform)
        {
            captureRenderer = this.GetComponent<ICaptureRenderer>();
            platform = transporterPlatform;
            timer.SetTimer(4);
        }

        private void FixedUpdate()
        {
            if (isPaused) return;
            if (captureState != null) captureState.RunState();
        }

        public void TransitionState(CaptureState state)
        {
            this.captureState = state;
            this.captureState.SetCaptureContext(this);
            this.captureState.InitState();
        }

        private void ResetCaptureSystem()
        {
            captureState = null;
            timer.ResetTimer();
            captureRenderer.StopRenderingCaptureVisual();
        }

        /// <summary>
        /// Called when invoked after Timer has completed
        /// </summary>
        public void SetActionsOnTimerCompletion()
        {
            shipLandingManoeuvre.LockMovement();
            captureRenderer.StopRenderingCaptureVisual();
            TransitionState(new GuideState());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            shipLandingManoeuvre = collision.gameObject.GetComponent<IAutoLandingManoeuvre>();
            shipTranslate = collision.gameObject.GetComponent<IShipPlatformTranslator>();
            shipPositionLocator = collision.gameObject.GetComponent<IShipPositionLocator>();
            platform.LoadPlayerCabin(collision.gameObject.GetComponent<IPlayerCabin>());

            TransitionState(new TrackingState());
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            ResetCaptureSystem();
        }

        /// <summary>
        /// Calls the necessary statements to release the playership when triggered.
        /// </summary>
        public void EndCapture()
        {
            shipLandingManoeuvre.UnlockMovement();
            platform.EndPlatformTransport();
            ResetCaptureSystem();
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

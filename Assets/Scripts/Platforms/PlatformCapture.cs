using System;
using UnityEngine;
using Evacuation.Actor.PlayerSystems;
using Evacuation.Weapons;

namespace Evacuation.Level.TransportSystems
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
        public ICaptureRenderer captureRenderer;
        public IAutoLandingSystem autoLandingSystem;
        public IShipLandingSystem shipLandingSystem;

        // Inspector Accessible Fields
        [SerializeField] private Timer timer;

        // Fields
        private bool isPaused = false;
        private CaptureState captureState = null;

        // Accessors
        public Timer CaptureTimer => timer;


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
            autoLandingSystem.LockMovement();
            captureRenderer.StopRenderingCaptureVisual();
            TransitionState(new GuideState());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            if (collision.GetComponent<IProjectile>() != null) return;

            autoLandingSystem = collision.gameObject.GetComponent<IAutoLandingSystem>();
            shipLandingSystem = collision.gameObject.GetComponent<IShipLandingSystem>();
            platform.LoadPlayerCabin(collision.gameObject.GetComponent<IPlayerCabin>());

            TransitionState(new TrackingState());
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            if (collision.GetComponent<IProjectile>() != null) return;

            ResetCaptureSystem();
        }

        /// <summary>
        /// Calls the necessary statements to release the playership when triggered.
        /// </summary>
        public void EndCapture()
        {
            autoLandingSystem.UnlockMovement();
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

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
        private ITransportPlatform platform;
        private IAutoLandingManoeuvre shipLandingManoeuvre;
        private ICaptureRenderer captureRenderer;

        // Inspector Accessible Fields
        [SerializeField] private Timer timer;

        // Fields
        private bool isCaptured = false;
        private bool isPaused = false;
        private bool isActive = false;

        public void InitialiseCaptureSystem(ITransportPlatform transporterPlatform)
        {
            captureRenderer = this.GetComponent<ICaptureRenderer>();
            platform = transporterPlatform;
            timer.SetTimer(4);
        }

        private void FixedUpdate()
        {
            if (!isActive || isPaused) return;

            if (isCaptured)
            {
                TriggerPlatformRescue();
                RunShipLandingGuidance();
            }
            else
            {
                captureRenderer.ShrinkCaptureCircle();
                captureRenderer.PositionCaptureLine();
            }
        }

        private void TriggerPlatformRescue()
        {
            if (shipLandingManoeuvre.CheckHasLanded())
            {
                platform.EnablePlatformTransport();

                // This is only required to be called once
                // as the platform handles its transferal operation
                isActive = false;
            }
        }
        
        private void RunShipLandingGuidance()
        {
            if (!shipLandingManoeuvre.CheckHasLanded())
            {
                shipLandingManoeuvre.AutoLand(this.transform);
            }
        }

        private void BeginCapture(IShipPositionLocator shipPositionLocator)
        {
            captureRenderer.BeginRenderingCaptureVisual(shipPositionLocator);
            timer.StartTimer();

            isActive = true;
            isCaptured = false;
        }

        private void StopCapture()
        {
            shipLandingManoeuvre = null;
            isActive = false;

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

            isCaptured = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            shipLandingManoeuvre = collision.gameObject.GetComponent<IAutoLandingManoeuvre>();
            IShipPositionLocator shipPositionLocator = collision.gameObject.GetComponent<IShipPositionLocator>();

            BeginCapture(shipPositionLocator);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            StopCapture();
        }

        /// <summary>
        /// Calls the necessary statements to release the playership when triggered.
        /// </summary>
        public void EndCapture()
        {
            platform.EndPlatformTransport();
            captureRenderer.StopRenderingCaptureVisual();

            isCaptured = false;
            isActive = false;
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

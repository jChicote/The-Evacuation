using UnityEngine;
using PlayerSystems;

namespace TransportSystems
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
        private IShipPositionLocator shipPositionLocator;

        // Inspector Accessible Fields
        [SerializeField] private Transform captureCircle;
        [SerializeField] private LineRenderer captureLine;
        [SerializeField] private Timer timer;

        // Fields
        private float scaleValue;
        private bool isCaptured = false;
        private bool isPaused = false;
        private bool isActive = false;

        public void InitialiseCaptureSystem(ITransportPlatform transporterPlatform)
        {
            platform = transporterPlatform;
            timer.SetTimer(4);
        }

        private void FixedUpdate()
        {
            if (!isActive || isPaused) return;

            if (isCaptured)
            {
                PerformPlatformRescueTransfer();
                RunShipLandingGuidance();
            }
            else
            {
                ShrinkCaptureCircle();
                PositionCaptureLine();
                timer.RunTimer();
            }
        }


        private void PerformPlatformRescueTransfer()
        {
            if (shipLandingManoeuvre.CheckHasLanded())
            {
                platform.EnablePlatformTransport();
            }
        }
        
        private void RunShipLandingGuidance()
        {
            if (!shipLandingManoeuvre.CheckHasLanded())
            {
                shipLandingManoeuvre.AutoLand(this.transform);
            }
        }

        private void ShrinkCaptureCircle()
        {
            scaleValue = Mathf.Lerp(captureCircle.localScale.x, 0, Time.fixedDeltaTime);
            captureCircle.localScale = new Vector2(scaleValue, scaleValue);
        }

        private void PositionCaptureLine()
        {
            captureLine.SetPosition(0, new Vector2(0, 0));
            captureLine.SetPosition(1, transform.InverseTransformPoint(shipPositionLocator.GetShipPosition()));
        }

        /// <summary>
        /// Called when invoked after Timer has completed
        /// </summary>
        public void SetActionsOnTimerCompletion()
        {
            shipLandingManoeuvre.LockMovement();
            ResetCaptureVisuals();
            isCaptured = true;
        }

        private void ResetCaptureVisuals()
        {
            captureCircle.gameObject.SetActive(false);
            captureCircle.localScale = new Vector2(1, 1);
            scaleValue = 1;

            captureLine.gameObject.SetActive(false); 
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            shipLandingManoeuvre = collision.gameObject.GetComponent<IAutoLandingManoeuvre>();
            shipPositionLocator = collision.gameObject.GetComponent<IShipPositionLocator>();

            ShrinkCaptureCircle();
            captureCircle.gameObject.SetActive(true);
            PositionCaptureLine();
            captureLine.gameObject.SetActive(true);

            isActive = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            shipLandingManoeuvre = null;
            shipPositionLocator = null;
            isActive = false;

            timer.ResetTimer();
            ResetCaptureVisuals();
        }

        /// <summary>
        /// Calls the necessary statements to release the playership when triggered.
        /// </summary>
        public void EndCapture()
        {
            platform.EndPlatoformTransport();
            ResetCaptureVisuals();

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

using Level.Collections;

namespace TransportSystems
{
    // Summary:
    //      This interface defines the enablers calls for controlling the transfer behavior.
    public interface ITransportPlatform
    {
        void EnablePlatformTransport();
        void EndPlatoformTransport();
    }

    // Summary:
    //      The PickupPlatform is the child of the BasePlatform and is responsible
    //      for pickup related actions, preferably found on islands.
    public class PickupPlatform : BasePlatform, ITransportPlatform
    {
        // Fields
        private float timeLeft;
        private float decrementSpeed;
        private bool canTransport;

        public override void InitialisePlatform(IRescueInhabitant islandInhabitants)
        {
            this.islandInhabitants = islandInhabitants;

            ICapture captureSystem = this.GetComponent<PlatformCapture>();
            captureSystem.InitialiseCaptureSystem(this);
        }

        private void FixedUpdate()
        {
            if (!CheckTransportValidity() || !canTransport) return;

            SimpleTimer();
            RunTransfer();

        }

        public override void RunTransfer()
        {
            base.RunTransfer();

            islandInhabitants.PickupIndividual();

        }

        private void SimpleTimer()
        {
            timeLeft -= decrementSpeed;
        }

        private bool CheckTransportValidity()
        {
            return timeLeft <= 0;
        }

        public void EnablePlatformTransport()
        {
            canTransport = true;
        }

        public void EndPlatoformTransport()
        {
            canTransport = false;
        }
    }
}

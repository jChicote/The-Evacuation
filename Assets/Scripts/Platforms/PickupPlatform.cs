using UnityEngine;
using Level.Collections;
using PlayerSystems;

namespace Level.TransportSystems
{
    // Summary:
    //      The PickupPlatform is the child of the BasePlatform and is responsible
    //      for pickup related actions, preferably found on islands.
    public class PickupPlatform : BasePlatform, ITransportPlatform, IPausable
    {
        public override void InitialisePlatform(IRescueInhabitant islandInhabitants)
        {
            this.islandInhabitants = islandInhabitants;

            ICapture captureSystem = this.GetComponent<ICapture>();
            captureSystem.InitialiseCaptureSystem(this);
        }

        public override void RunTransfer()
        {
            base.RunTransfer();

            if (playerCabin.CheckAtMaxCapacity()) 
                return;

            islandInhabitants.PickupIndividual();
        }

        // This class should only be called once to be enabled.
        public override void EnablePlatformTransport()
        {
            base.EnablePlatformTransport();
        }

        // This class should only be called once to be disabled.
        public override void EndPlatformTransport()
        {
            base.EndPlatformTransport();
        }

        public override void OnPause()
        {
            base.OnPause();
        }

        public override void OnUnpause()
        {
            base.OnUnpause();
        }
    }
}

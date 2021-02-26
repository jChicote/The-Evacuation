using UnityEngine;
using Level.Collections;
using PlayerSystems;

namespace Level.TransportSystems
{
    public class DropoffPlatform : BasePlatform
    {
        public override void InitialisePlatform(IRescueInhabitant islandInhabitants)
        {
            this.islandInhabitants = islandInhabitants;

            ICapture captureSystem = this.GetComponent<PlatformCapture>();
            captureSystem.InitialiseCaptureSystem(this);
        }

        public override void RunTransfer()
        {
            base.RunTransfer();

            if (playerCabin.CheckIsEmpty())
                return;

            islandInhabitants.DropOffIndividual();
        }

        public override void EnablePlatformTransport()
        {
            base.EndPlatformTransport();
        }

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
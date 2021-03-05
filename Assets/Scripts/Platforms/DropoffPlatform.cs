using UnityEngine;
using Evacuation.Level.Collections;
using Evacuation.PlayerSystems;

namespace Evacuation.Level.TransportSystems
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

            if (playerCabin.CheckIsEmpty()) return;

            islandInhabitants.DropOffIndividual();
            playerCabin.DepartFromShipCabin();
        }

        public override void EnablePlatformTransport()
        {
            base.EnablePlatformTransport();
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
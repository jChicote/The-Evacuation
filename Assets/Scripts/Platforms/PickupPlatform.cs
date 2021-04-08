using UnityEngine;
using Evacuation.Level.Collections;
using Evacuation.Actor.PlayerSystems;
using Evacuation.UserInterface.LocationMarker;

namespace Evacuation.Level.TransportSystems
{
    // Summary:
    //      The PickupPlatform is the child of the BasePlatform and is responsible
    //      for pickup related actions, preferably found on islands.
    public class PickupPlatform : BasePlatform, ITransportPlatform, IPausable
    {
        public override void InitialisePlatform(IRescueInhabitant islandInhabitants)
        {
            this.islandInhabitants = islandInhabitants;

            GameManager.Instance.sceneController.markerManager.GenerateLocationMarker(this.transform, MarkerType.Island);
            ICapture captureSystem = this.GetComponent<ICapture>();
            captureSystem.InitialiseCaptureSystem(this);
        }

        public override void RunTransfer()
        {
            base.RunTransfer();

            if (playerCabin.CheckAtMaxCapacity()) return;

            islandInhabitants.PickupIndividual();
            playerCabin.AddToShipCabin();
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

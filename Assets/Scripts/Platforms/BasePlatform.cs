using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Level.Collections;
using Evacuation.Actor.PlayerSystems;
using Evacuation.UserInterface.LocationMarker;

namespace Evacuation.Level.TransportSystems
{
    // Summary:
    //      This interface defines the enablers calls for controlling the transfer behavior.
    public interface ITransportPlatform
    {
        void LoadPlayerCabin(IPlayerCabin playerCabin);
        void EnablePlatformTransport();
        void EndPlatformTransport();
    }

    public abstract class BasePlatform : MonoBehaviour, ITransportPlatform, IPausable
    {
        // Interfaces
        public IRescueInhabitant islandInhabitants;
        public IPlayerCabin playerCabin;
        protected IMarkerManager pointMarkerManager;

        // Inspector Accessible Fields
        [SerializeField] protected Transform landingPoint;

        // Fields
        protected bool canTransport;


        public abstract void InitialisePlatform(IRescueInhabitant islandInhabitants);

        public virtual void RunTransfer() 
        {
            if (playerCabin == null) return;
        }

        public void LoadPlayerCabin(IPlayerCabin playerCabin)
        {
            this.playerCabin = playerCabin;
        }

        public virtual void EnablePlatformTransport()
        {
            canTransport = true;
            InvokeRepeating(nameof(RunTransfer), 0, 1.2f);
        }

        public virtual void EndPlatformTransport()
        {
            canTransport = false;
            CancelInvoke();
        }

        public virtual void OnPause() 
        {
            CancelInvoke();
        }

        public virtual void OnUnpause() 
        {
            if (canTransport)
            {
                InvokeRepeating(nameof(RunTransfer), 0, 1.2f);
            }
        }
    }
}

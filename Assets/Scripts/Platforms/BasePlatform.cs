using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Level.Collections;

namespace Level.TransportSystems
{
    public abstract class BasePlatform : MonoBehaviour, ITransportPlatform, IPausable
    {
        // Inspector Accessible Fields
        [SerializeField] protected Transform landingPoint;
        [SerializeField] protected IRescueInhabitant islandInhabitants;

        // Fields
        protected bool canTransport;

        public abstract void InitialisePlatform(IRescueInhabitant islandInhabitants);

        public virtual void RunTransfer() { }

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

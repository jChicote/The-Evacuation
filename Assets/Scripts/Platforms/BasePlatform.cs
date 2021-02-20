using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Level.Collections;

namespace TransportSystems
{
    public abstract class BasePlatform : MonoBehaviour
    {
        [SerializeField] protected Transform landingPoint;
        [SerializeField] protected IRescueInhabitant islandInhabitants;

        public abstract void InitialisePlatform(IRescueInhabitant islandInhabitants);
        public virtual void RunTransfer() { }
    }
}

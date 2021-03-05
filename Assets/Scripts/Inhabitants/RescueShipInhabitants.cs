using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Level.TransportSystems;

namespace Evacuation.Level.Collections
{
    public class RescueShipInhabitants : MonoBehaviour, IAbstractInhabitants, IRescueInhabitant
    {
        // Inspector Accessible Fields
        private int inhabitantCount = 0;

        // Fields
        private ScoreSystem scoreSystem;

        public void InitialiseIsland(ScoreSystem scoreSystem)
        {
            BasePlatform platform = this.GetComponentInChildren<BasePlatform>();
            platform.InitialisePlatform(this);

            // Gets the score system without the singleton, to limit the chaining on 
            this.scoreSystem = scoreSystem;
        }

        public void DropOffIndividual()
        {
            inhabitantCount++;
            scoreSystem.IncrementRescueCount();
        }

        public void PickupIndividual()
        {
            if (inhabitantCount <= 0)
                return;

            inhabitantCount--;
        }
    }
}

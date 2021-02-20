using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransportSystems;


namespace Level.Collections
{
    public interface IRescueInhabitant
    {
        void PickupIndividual();
        void DropOffIndividual();
    }

    public class IslandArea : MonoBehaviour, IRescueInhabitant
    {
        [SerializeField] private int inhabitantCount = 0;

        private void Awake()
        {
            InitialiseIsland();
        }

        public void InitialiseIsland()
        {
            BasePlatform platform = this.GetComponentInChildren<BasePlatform>();
            platform.InitialisePlatform(this);
        }

        public void DropOffIndividual()
        {
            inhabitantCount++;
        }

        public void PickupIndividual()
        {
            inhabitantCount--;
        }
    }
}

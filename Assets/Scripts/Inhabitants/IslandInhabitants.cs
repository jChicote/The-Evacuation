using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Level.TransportSystems;


namespace Evacuation.Level.Collections
{
    // Summary:
    //      This interface abstracts island functionality to operate
    //      with different variant concrete types.
    public interface IAbstractInhabitants
    {
        void InitialiseIsland(ScoreSystem scoreSystem);
    }

    // Summary:
    //      This interface is designed to pull and dop inhabitants from the player.
    public interface IRescueInhabitant
    {
        void PickupIndividual();
        void DropOffIndividual();
    }

    public class IslandInhabitants : MonoBehaviour, IRescueInhabitant, IAbstractInhabitants
    {
        // Inspector Accessible Fields
        [SerializeField] private int inhabitantCount = 0;

        // Fields
        private ScoreSystem scoreSystem;

        /// <summary>
        /// Initialises island's inhabitants and transport system on Awake.
        /// </summary>
        public void InitialiseIsland(ScoreSystem scoreSystem)
        {
            // THIS MAY LATER NEED TO BE SEPERATELY CALLED FROM THE PLATFORM
            // As these are both seperate entities from eachother.

            BasePlatform platform = this.GetComponentInChildren<BasePlatform>();
            platform.InitialisePlatform(this);

            // Gets the score system without the singleton, to limit the chaining on 
            this.scoreSystem = scoreSystem;
        }

        public void DropOffIndividual()
        {
            inhabitantCount++;
        }

        public void PickupIndividual()
        {
            if (inhabitantCount <= 0) 
                return;

            inhabitantCount--;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Evacuation.Actor;

namespace Evacuation.Model
{
    public class ShipDataServicer : MonoBehaviour
    {
        private SessionData instance;

        private List<ShipInfo> hangarShips = new List<ShipInfo>();
        private ShipInfo selectedShip;

        public ShipDataServicer()
        {
            this.instance = SessionData.instance;
        }

        public void SetHangarShips(List<ShipInfo> loadedShips)
        {
            this.hangarShips = loadedShips;
        }

        /// <summary>
        /// Accessor for ship's info data.
        /// </summary>
        public ShipInfo GetSelectedShip()
        {
            return selectedShip;
        }

        /// <summary>
        /// Accessor for specific ship item from hangar.
        /// </summary>
        public ShipInfo GetShipItem(string shipID)
        {
            // As there are no duplicates storing ship identifiers through string, hangar will only store id for simplification.
            // Only will fetch and store ship info of vessels that are either in use or unlocked.

            return hangarShips.Where(x => x.stringID == shipID).First();
        }

        /// <summary>
        /// Accessor for retrieving entire ship hangar.
        /// </summary>
        public List<ShipInfo> GetHangarShips()
        {
            return hangarShips;
        }

        /// <summary>
        /// Resets all ships currently in this class.
        /// </summary>
        public void ResetAllShips()
        {
            // Resets vessels to all preset unlocked states
            hangarShips.Clear();

            foreach (ShipAsset asset in GameManager.Instance.playerSettings.shipsList)
            {
                hangarShips.Add(asset.ConvertToShipInfo());
            }
        }
    }
}

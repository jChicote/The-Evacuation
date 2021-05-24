using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Evacuation.Session;

namespace Evacuation.Actor.PlayerSystems
{
    public interface ISpawnManager
    {
        void InitialisePlayerSpawner();
    }

    public class PlayerSpawnManager : MonoBehaviour, ISpawnManager
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private Transform spawnPosition;

        public void InitialisePlayerSpawner()
        {
            if (player == null)
            {
                CreatePlayer();
                print("Player has been Spawned");
            }
        }

        private void CreatePlayer()
        {
            SessionData sessionData = SessionData.instance;
            ShipAsset asset = GameManager.Instance.playerSettings.shipsList.Where(x => x.stringID == sessionData.selectedShip.stringID).First();
            GameObject playerObject = Instantiate(asset.shipPrefab, transform.position, Quaternion.identity);
            player = playerObject.GetComponent<PlayerController>();
        }
    }
}

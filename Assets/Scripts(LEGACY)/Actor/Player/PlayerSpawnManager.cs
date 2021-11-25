using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Evacuation.Model;

public interface ISpawnManager
{
    void InitialiseSpawner();
}

namespace Evacuation.Actor.PlayerSystems
{

    public class PlayerSpawnManager : MonoBehaviour, ISpawnManager
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private Transform spawnPosition;

        public void InitialiseSpawner()
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
            ShipAsset asset = GameManager.Instance.playerSettings.shipsList.Where(x => x.instanceID == sessionData.selectedShip.instanceID).First();
            GameObject playerObject = Instantiate(asset.shipPrefab, transform.position, Quaternion.identity);
            player = playerObject.GetComponent<PlayerController>();
        }
    }
}

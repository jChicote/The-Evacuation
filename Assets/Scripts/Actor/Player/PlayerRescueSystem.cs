using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Level.TransportSystems;

namespace Evacuation.PlayerSystems
{
    public interface IPlayerCabin
    {
        void AddToShipCabin();
        void DepartFromShipCabin();
        bool CheckAtMaxCapacity();
        bool CheckIsEmpty();
    }

    public interface IShipPlatformTranslator
    {
        void AttachToPlatform();
        void StickToPlatform(Vector3 platformPosition);
    }

    public interface IShipToPlatformAction
    {
        void DetachFromPlatform();
    }

    public class PlayerRescueSystem : MonoBehaviour, IShipPlatformTranslator, IPlayerCabin, IShipToPlatformAction
    {
        private ICapture endTransport;
        private IStatHandler statHandler;
        private IWeaponLoadoutSelector loadoutSelector;

        public void InitialiseRescueSsytem()
        {
            statHandler = this.GetComponent<IStatHandler>();
            loadoutSelector = this.GetComponent<IWeaponLoadoutSelector>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Must check on both parent and children object if rigidbody is combining colliders
            if (!collision.CompareTag("Platform")) return;
            
            endTransport = collision.gameObject.GetComponent<ICapture>();
                
            if (endTransport == null)
                endTransport = collision.gameObject.GetComponentInChildren<ICapture>();
        }

        public void StickToPlatform(Vector3 platformPosition)
        {
            transform.position = platformPosition;
        }

        public void AttachToPlatform()
        {
            loadoutSelector.ChooseLoadoutPosition(LoadoutPosition.Pivot);
        }

        public void DetachFromPlatform()
        {
            if (endTransport == null) return;

            loadoutSelector.ChooseLoadoutPosition(LoadoutPosition.Forward);
            endTransport.EndCapture();
            endTransport = null;
        }

        public void AddToShipCabin()
        {
            statHandler.RescueCabinCount++;
        }

        public void DepartFromShipCabin()
        {
            statHandler.RescueCabinCount--;
        }

        public bool CheckAtMaxCapacity()
        {
            return statHandler.RescueCabinCount == statHandler.CabinCapacity;
        }

        public bool CheckIsEmpty()
        {
            return statHandler.RescueCabinCount == 0;
        }
    }
}
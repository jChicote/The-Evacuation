using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Level.TransportSystems;
using Evacuation.Cinematics.CameraUtil;

namespace Evacuation.Actor.PlayerSystems
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

        private ICameraZoom cameraZoom;

        public void InitialiseRescueSsytem()
        {
            statHandler = this.GetComponent<IStatHandler>();
            loadoutSelector = this.GetComponent<IWeaponLoadoutSelector>();
            cameraZoom = this.GetComponentInChildren<ICameraZoom>();
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
            loadoutSelector.ChooseLoadoutPosition(LoadoutConfiguration.Pivot);
            cameraZoom.SetTargetZoom(6.5f);
        }

        public void DetachFromPlatform()
        {
            if (endTransport == null) return;

            cameraZoom.SetToDefaultZoom();
            loadoutSelector.ChooseLoadoutPosition(LoadoutConfiguration.Forward);
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
            return statHandler.RescueCabinCount == statHandler.GetShipData().RescueCapacity;
        }

        public bool CheckIsEmpty()
        {
            return statHandler.RescueCabinCount == 0;
        }
    }
}
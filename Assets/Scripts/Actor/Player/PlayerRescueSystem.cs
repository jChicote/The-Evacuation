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

    public interface IShipLandingSystem : IShipToPlatformAction
    {
        void AttachToPlatform();
        void StickToPlatform(Vector3 platformPosition);
    }

    public interface IShipToPlatformAction
    {
        void DetachFromPlatform();
    }

    public class PlayerRescueSystem : MonoBehaviour, IShipLandingSystem, IPlayerCabin
    {
        // Interfaces
        private ICapture capturePlatform;
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

            capturePlatform = collision.gameObject.GetComponent<ICapture>();
                
            if (capturePlatform == null)
                capturePlatform = collision.gameObject.GetComponentInChildren<ICapture>();
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
            if (capturePlatform == null) return;

            cameraZoom.SetToDefaultZoom();
            loadoutSelector.ChooseLoadoutPosition(LoadoutConfiguration.Forward);
            capturePlatform.EndCapture();
            capturePlatform = null;
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
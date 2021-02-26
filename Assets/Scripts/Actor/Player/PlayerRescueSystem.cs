using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Level.TransportSystems;

namespace PlayerSystems
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
        void StickToPlatform(Vector3 platformPosition);
    }

    public class PlayerRescueSystem : MonoBehaviour, IShipPlatformTranslator, IPlayerCabin
    {
        private ICapture endTransport;
        private IStatHandler statHandler;

        public void InitialiseRescueSsytem()
        {
            statHandler = this.GetComponent<IStatHandler>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Platform"))
            {
                endTransport = this.GetComponent<ICapture>();
            }
        }

        public void StickToPlatform(Vector3 platformPosition)
        {
            transform.position = platformPosition;
        }

        public void EndTransport()
        {
            if (endTransport == null) return;

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
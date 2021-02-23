using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Level.TransportSystems;

namespace PlayerSystems
{
    public interface ITransportToCabin
    {

    }

    public class PlayerRescueSystem : MonoBehaviour
    {
        private ICapture endTransport;
        private IStatHandler statHandler;

        public void InitialiseRescueSsytem()
        {
            statHandler = this.GetComponent<IStatHandler>();
        }

        public void EndTransport()
        {
            if (endTransport == null) return;

            endTransport.EndCapture();
            endTransport = null;
        }

        public void AddToShipCabin()
        {

        }

        public void DepartFromShipCabin()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Platform"))
            {
                endTransport = this.GetComponent<ICapture>();
            }
        }
    }
}
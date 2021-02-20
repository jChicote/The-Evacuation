using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransportSystems;

namespace PlayerSystems
{

    public class PlayerRescueSystem : MonoBehaviour
    {
        private ICapture endTransport;

        public void EndTransport()
        {
            if (endTransport == null) return;

            endTransport.EndCapture();
            endTransport = null;
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
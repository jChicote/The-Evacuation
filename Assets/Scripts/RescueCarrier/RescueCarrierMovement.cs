using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level.Collections
{

    public interface ICarrierInitialiser
    {
        void InitialiseCarrier();
    }

    public class RescueCarrierMovement : MonoBehaviour, IPausable, ICarrierInitialiser
    {
        // Inspector Accessible Fields
        [SerializeField] private float carrierSpeed;

        // Fields
        private Rigidbody2D carrierRB;
        private Vector2 carrierVelocity;

        private bool isPaused = false;

        public void InitialiseCarrier()
        {
            carrierRB = this.GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            DoMovment();
        }

        private void DoMovment()
        {
            if (isPaused) return;

            carrierVelocity = Vector2.right * carrierSpeed;
            carrierRB.velocity = carrierVelocity;
        }

        public void OnPause()
        {
            isPaused = true;
        }

        public void OnUnpause()
        {
            isPaused = false;
        }
    }
}

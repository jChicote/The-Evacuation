using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Weapons
{
    public class Bullet : Projectile
    {
        public override void InitialiseProjectile(float speed, float damage, float life)
        {
            base.InitialiseProjectile(speed, damage, life);
        }

        private void FixedUpdate()
        {
            if (isPaused)
            {
                projectileRB.velocity = Vector2.zero;
                return;
            }

            projectileRB.velocity = transform.up * speed;

            SelfDestructTimer();
        }

        /// <summary>
        /// Counts till the life of the projectile has expired.
        /// </summary>
        public override void SelfDestructTimer()
        {
            base.SelfDestructTimer();

            if (life <= 0)
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Responsible for calculating the damage inflicted to the colliding object.
        /// </summary>
        /// <param name="damageInstance"></param>
        public override void InflictDamage(IDamageable damageInstance)
        {
            base.InflictDamage(damageInstance);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(this.tag)) return;

            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable == null) return;

            InflictDamage(damageable);
        }
    }
}

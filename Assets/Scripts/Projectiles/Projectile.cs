using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    void InitialiseProjectile(float speed, float damage, float life);
    void SetTag(string tag);
}

namespace Evacuation.Weapons
{
    public abstract class Projectile : MonoBehaviour, IProjectile, IPausable
    {
        public Rigidbody2D projectileRB;

        // Fields
        protected float speed;
        protected float damage;
        protected float life;
        protected bool isPaused = false;

        public virtual void InitialiseProjectile(float speed, float damage, float life)
        {
            this.speed = speed;
            this.damage = damage;
            this.life = life;
        }

        public virtual void SelfDestructTimer()
        {
            life -= Time.fixedDeltaTime;
        }

        public virtual void InflictDamage(IDamageable damageInstance)
        {
            damageInstance.OnDamage(damage);
        }

        public void SetTag(string tag)
        {
            this.tag = tag;
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

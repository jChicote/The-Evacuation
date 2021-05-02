using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void InitialiseComponent();
    void OnDamage(float damage);
}

namespace Evacuation.Actor
{
    public class ActorDamageController : MonoBehaviour, IDamageable
    {
        public virtual void InitialiseComponent() { }

        public virtual void OnDamage(float damage) { }
    }
}

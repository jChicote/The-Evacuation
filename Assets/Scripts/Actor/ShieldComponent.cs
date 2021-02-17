using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShieldComponent : MonoBehaviour
{
    protected bool isShieldRegenerating = false;

    public virtual void InitialiseShield(float maxShield) { }

    public virtual void SetShieldUpdate(float healthValue) { }
}

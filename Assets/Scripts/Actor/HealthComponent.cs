using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{

    protected bool isHealthRegenerating = false;

    public virtual void InitialiseHealth(float maxHealth) { }

    public virtual void SetHealthUpdate(float healthValue) { }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Actor;

public class EnemyHealthComponent : HealthComponent
{
    public override void InitialiseHealth(float maxHealth) 
    { 
    }

    public override void SetHealthUpdate(float healthValue) 
    {
    }

    public float CalculateDamagedHealth(float damage)
    {
        return damage;
    }

    public bool IsActive()
    {
        return false;
    }
}

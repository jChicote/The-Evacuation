using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Actor;

public class EnemyHealthComponent : HealthComponent
{
    private IHealthAccessors healthAccessors;

    public override void InitialiseHealth(float maxHealth) 
    {
        healthAccessors = this.GetComponent<IHealthAccessors>();
    }

    public override void SetHealthUpdate(float healthValue) 
    {
        healthAccessors.SetShipHealth(healthValue);
        Debug.Log("Enemy Health At: " + healthValue);
    }

    public float CalculateDamagedHealth(float damage)
    {
        return healthAccessors.GetShipHealth() - damage;
    }

    public bool IsActive()
    {
        return healthAccessors.GetShipHealth() > 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    protected enum DamageType {
        Normal,
        Fire, 
        Ice, 
        Electric,
        Poison
    }

    [SerializeField] private float damage;
    [SerializeField] private DamageType damageType;
    
    private void OnTriggerEnter(Collider other) 
    {
        HealthManager otherHealth = other.gameObject.GetComponent<HealthManager>();

        if (otherHealth == null)
            return;

        otherHealth.ApplyDamage(damage, damageType.ToString());
    }

    private void OnCollisionEnter(Collision other) 
    {
        HealthManager otherHealth = other.gameObject.GetComponent<HealthManager>();

        if (otherHealth == null)
            return;

        otherHealth.ApplyDamage(damage, damageType.ToString());
    }
}

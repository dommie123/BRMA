using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float health;

    public void ApplyDamage(float damage)
    {
        ApplyDamage(damage, "Normal");
    }

    public void ApplyDamage(float damage, string damageType)
    {
        Debug.Log(damageType);
        health -= Mathf.Abs(damage);
    }

    public bool EntityIsDead()
    {
        return health <= 0;
    }

    public float GetHealth()
    {
        return health;
    }
}

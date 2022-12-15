using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float burnChance;
    [SerializeField] private float psnChance;

    public void ApplyDamage(float damage)
    {
        ApplyDamage(damage, "Normal");
    }

    public void ApplyDamage(float damage, string damageType)
    {
        health -= Mathf.Abs(damage);

        switch (damageType) 
        {
            case "Fire":
                Debug.Log("I got burned!");
                break;
            case "Poison":
                Debug.Log("I got poisoned!");
                break;
            case "Ice":
                Debug.Log("I got frozen!");
                break;
            default: 
                Debug.Log("I got hurt!");
                break;
        }
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

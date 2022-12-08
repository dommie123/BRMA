using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    [SerializeField] private float psnInterval;
    [SerializeField] private float brnSeconds;

    private List<StatusEffect> currentStatusEffects;
    private float psnTimer;
    private float brnTimer;
    private HealthManager entityHealth;

    private void Awake()
    {
        currentStatusEffects = new List<StatusEffect>();
        entityHealth = GetComponent<HealthManager>();
        psnTimer = psnInterval;
        brnTimer = brnSeconds;
    }

    private void Update() 
    {
        if (currentStatusEffects.Contains(StatusEffect.Poison))
        {
            psnTimer -= Time.deltaTime;

            if (psnTimer <= 0)
            {
                psnTimer = psnInterval;
                entityHealth.ApplyDamage(1);
            }
        }
        else if (currentStatusEffects.Contains(StatusEffect.Burned))
        {
            brnTimer -= Time.deltaTime;
            entityHealth.ApplyDamage(1);

            if (brnTimer <= 0)
            {
                CureStatusEffect(StatusEffect.Burned);
            }
        }
    }

    public List<StatusEffect> GetCurrentStatusEffects()
    {
        return currentStatusEffects;
    }

    public void AddStatusEffect(StatusEffect effect)
    {
        currentStatusEffects.Add(effect);
    }

    public void CureStatusEffect(StatusEffect effect)
    {
        currentStatusEffects.Remove(effect);
    }

    public void CureAllStatusEffects()
    {
        currentStatusEffects.Clear();
    }
}

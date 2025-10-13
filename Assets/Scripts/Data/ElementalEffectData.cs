using System;
using UnityEngine;

[Serializable]
public class ElementalEffectData
{
    public float chillDuration;
    public float chillSlowMultiplier;

    public float totalBurnDuration;
    public float totalBurnDamage;

    public float shockDuration;
    public float shockDamage;
    public float shockCharge;

    public ElementalEffectData(Entity_Stats entitiyStats, DamageScaleData damageScale)
    {
        chillDuration = damageScale.chillDuration;
        chillSlowMultiplier = damageScale.chillSlowMultiplier;

        totalBurnDuration = damageScale.burnDuration;
        totalBurnDamage = entitiyStats.offence.fireDamage.GetValue() * damageScale.burnDamageScale;



        shockDuration = damageScale.shockDuration;
        shockDamage = entitiyStats.offence.lightningDamage.GetValue() * damageScale.shockDamageScale;
        shockCharge = damageScale.shockCharge;

    }
}
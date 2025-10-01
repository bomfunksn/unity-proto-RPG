using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHp;
    public Stat_MajorGroup major;
    public Stat_OffenceGroup offence;
    public Stat_DefenceGroup defence;

    public float GetPhysicalDamage(out bool isCrit)
    {
        float baseDamage = offence.damage.GetValue();
        float bonusDamage = major.strength.GetValue();
        float totalBaseDamage = baseDamage + bonusDamage;

        float baseCritChance = offence.critChance.GetValue();
        float bonusCritChance = major.agility.GetValue() * .3f;
        float critChance = baseCritChance + bonusCritChance;

        float baseCritPower = offence.critPower.GetValue();
        float bonusCritPower = major.strength.GetValue() * .5f;
        float critPower = (baseCritPower + bonusCritPower) / 100;

        isCrit = Random.Range(0, 100) < critChance;
        float finalDamage = isCrit ? totalBaseDamage * critPower : totalBaseDamage;

        return finalDamage;
    }

    public float GetArmorMitigation(float armorReduction)
    {
        float baseArmor = defence.armor.GetValue();
        float bonusArmor = major.vitality.GetValue(); //+1 per point
        float totalArmor = baseArmor + bonusArmor;

        float reductionMultiplier = Mathf.Clamp (1 - armorReduction,0,1);
        float effectiveArmor = totalArmor * reductionMultiplier;

        float mitigation = effectiveArmor / (effectiveArmor + 100);
        float mitigationCap = .85f; //85%

        float finalMititgation = Mathf.Clamp(mitigation, 0, mitigationCap);
        return finalMititgation;
    }

    public float GetArmorReduction()
    {
        float finalReduction = offence.armorReduction.GetValue() / 100;
        return finalReduction;
    }

    public float GetMaxHealth()
    {
        float baseMaxHealth = maxHp.GetValue();
        float bonusMaxHealth = major.vitality.GetValue() * 5;
        float finalMaxHealth = baseMaxHealth + bonusMaxHealth;

        return finalMaxHealth;
    }

    public float GetEvasion()
    {
        float baseEvasion = defence.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * .5f;

        float totalEvasion = baseEvasion + bonusEvasion;
        float evasionCap = 85f;

        float finalEvasion = Mathf.Clamp(totalEvasion, 0, evasionCap);

        return finalEvasion;
    }

}

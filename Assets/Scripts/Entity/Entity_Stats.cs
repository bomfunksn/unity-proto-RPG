using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat_SetupSO defaultStatSetup;


    public Stat_ResourceGroup resources;
    public Stat_OffenceGroup offence;
    public Stat_DefenceGroup defence;
    public Stat_MajorGroup major;

    public float GetElementalDamage(out ElementType element, float scaleFactor = 1f)
    {
        float fireDamage = offence.fireDamage.GetValue();
        float iceDamage = offence.iceDamage.GetValue();
        float lightningDamage = offence.lightningDamage.GetValue();
        float bonusElementalDamage = major.intelligence.GetValue(); //+1 for int point

        float highestDamage = fireDamage;
        element = ElementType.Fire;

        if (iceDamage > highestDamage)
        {
            highestDamage = iceDamage;
            element = ElementType.Ice;
        }
        if (lightningDamage > highestDamage)
        {
            highestDamage = lightningDamage;
            element = ElementType.Lightning;
        }
        if (highestDamage <= 0)
        {
            element = ElementType.none;
            return 0;
        }

        float bonusFire = (element == ElementType.Fire) ? 0 : fireDamage * 0.5f;
        float bonusIce = (element == ElementType.Ice) ? 0 : iceDamage * 0.5f;
        float bonusLightning = (element == ElementType.Lightning) ? 0 : lightningDamage * 0.5f;

        float weakerElementsDamage = bonusFire + bonusIce + bonusLightning;
        float finalDamage = highestDamage + weakerElementsDamage + bonusElementalDamage;

        return finalDamage * scaleFactor;
    }

    public float GetElementalResistance(ElementType element)
    {
        float baseResistance = 0;
        float bonusResistance = major.intelligence.GetValue() * .5f; //0.5% per INT point

        switch (element)
        {
            case ElementType.Fire:
                baseResistance = defence.fireRes.GetValue();
                break;
            case ElementType.Ice:
                baseResistance = defence.iceRes.GetValue();
                break;
            case ElementType.Lightning:
                baseResistance = defence.lightningRes.GetValue();
                break;
        }
        float resistance = baseResistance + bonusResistance;
        float resistanceCap = 75f;
        float finalResistance = Mathf.Clamp(resistance, 0, resistanceCap) / 100;

        return finalResistance;
    }

    public float GetPhysicalDamage(out bool isCrit, float scaleFactor = 1f)
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

        return finalDamage * scaleFactor;
    }

    public float GetArmorMitigation(float armorReduction)
    {
        float baseArmor = defence.armor.GetValue();
        float bonusArmor = major.vitality.GetValue(); //+1 per point
        float totalArmor = baseArmor + bonusArmor;

        float reductionMultiplier = Mathf.Clamp(1 - armorReduction, 0, 1);
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
        float baseMaxHealth = resources.maxHealth.GetValue();
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

    public Stat GetStatByType(StatType type)
    {
        switch (type)
        {
            case StatType.MaxHealth: return resources.maxHealth;
            case StatType.HealthRegen: return resources.healthRegen;

            case StatType.Strenghth: return major.strength;
            case StatType.Agility: return major.agility;
            case StatType.Intellegence: return major.intelligence;
            case StatType.Vitality: return major.vitality;

            case StatType.AttackSpeed: return offence.attackSpeed;
            case StatType.Damage: return offence.damage;
            case StatType.CritChance: return offence.critChance;
            case StatType.CritPower: return offence.critPower;
            case StatType.ArmorReduction: return offence.armorReduction;

            case StatType.FireDamage: return offence.fireDamage;
            case StatType.IceDamage: return offence.iceDamage;
            case StatType.LightningDamage: return offence.lightningDamage;

            case StatType.Armor: return defence.armor;
            case StatType.Evasion: return defence.evasion;

            case StatType.IceResistance: return defence.iceRes;
            case StatType.FireResistance: return defence.fireRes;
            case StatType.LightningResistance: return defence.lightningRes;

            default:
                Debug.LogWarning($"StatType {type} not implemented");
                return null;
        }
    }

[ContextMenu("Update Default Stat Setup")]
    public void ApplyStatSetup()
    {
        if (defaultStatSetup == null)
        {
            Debug.LogWarning("No default stat setup assigned");
            return;
        }
        resources.maxHealth.SetBaseValue(defaultStatSetup.maxHealth);
        resources.healthRegen.SetBaseValue(defaultStatSetup.healthRegen);

        major.strength.SetBaseValue(defaultStatSetup.strength);
        major.agility.SetBaseValue(defaultStatSetup.agility);
        major.intelligence.SetBaseValue(defaultStatSetup.intelligence);
        major.vitality.SetBaseValue(defaultStatSetup.vitality);

        offence.attackSpeed.SetBaseValue(defaultStatSetup.attackSpeed);
        offence.damage.SetBaseValue(defaultStatSetup.damage);
        offence.critChance.SetBaseValue(defaultStatSetup.critChance);
        offence.critPower.SetBaseValue(defaultStatSetup.critPower);
        offence.armorReduction.SetBaseValue(defaultStatSetup.armorReduction);

        offence.fireDamage.SetBaseValue(defaultStatSetup.fireDamage);
        offence.iceDamage.SetBaseValue(defaultStatSetup.iceDamage);
        offence.lightningDamage.SetBaseValue(defaultStatSetup.lightningDamage);

        defence.armor.SetBaseValue(defaultStatSetup.armor);
        defence.evasion.SetBaseValue(defaultStatSetup.evasion);

        defence.fireRes.SetBaseValue(defaultStatSetup.fireResistance);
        defence.iceRes.SetBaseValue(defaultStatSetup.iceResistance);
        defence.lightningRes.SetBaseValue(defaultStatSetup.lightningResistance);
    }
}

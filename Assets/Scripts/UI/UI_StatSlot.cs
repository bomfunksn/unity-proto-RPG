using TMPro;
using UnityEngine;

public class UI_StatSlot : MonoBehaviour
{
    private Entity_Stats playerStats;
    private RectTransform rect;
    private UI ui;

    [SerializeField] private StatType statSlotType;
    [SerializeField] private TextMeshProUGUI statName;
    [SerializeField] private TextMeshProUGUI statValue;

    private void OnValidate()
    {
        gameObject.name = "UI_Stat - " + GetStatNameByType(statSlotType);
        statName.text = GetStatNameByType(statSlotType);

    }

    private void Awake()
    {
        ui = GetComponentInParent<UI>();
        rect = GetComponentInParent<RectTransform>();
        playerStats = FindAnyObjectByType<Entity_Stats>();
    }

    public void UpdateStatValue()
    {
        Stat statToUpdate = playerStats.GetStatByType(statSlotType);

        if (statToUpdate == null && statSlotType != StatType.ElementalDamage)
        {
            Debug.Log($"U havent {statSlotType} implemetned on the player");
            return;
        }

            float value = 0;

        switch (statSlotType)
        {
            //Major
            case StatType.Strength:
                value = playerStats.major.strength.GetValue();
                break;
            case StatType.Agility:
                value = playerStats.major.agility.GetValue();
                break;
            case StatType.Intellegence:
                value = playerStats.major.intelligence.GetValue();
                break;
            case StatType.Vitality:
                value = playerStats.major.vitality.GetValue();
                break;

            //Offence
            case StatType.Damage:
                value = playerStats.GetBaseDamage();
                break;
            case StatType.CritChance:
                value = playerStats.GetCritChance();
                break;
            case StatType.ArmorReduction:
                value = playerStats.GetArmorReduction() * 100;
                break;
            case StatType.AttackSpeed:
                value = playerStats.offence.attackSpeed.GetValue() * 100;
                break;

            //defence
            case StatType.MaxHealth:
                value = playerStats.GetMaxHealth();
                break;
            case StatType.HealthRegen:
                value = playerStats.resources.healthRegen.GetValue();
                break;
            case StatType.Evasion:
                value = playerStats.GetEvasion();
                break;
            case StatType.Armor:
                value = playerStats.GetBaseArmor();
                break;

            //elemental;
            case StatType.IceDamage:
                value = playerStats.offence.iceDamage.GetValue();
                break;
            case StatType.FireDamage:
                value = playerStats.offence.fireDamage.GetValue();
                break;
            case StatType.LightningDamage:
                value = playerStats.offence.lightningDamage.GetValue();
                break;
            case StatType.ElementalDamage:
                value = playerStats.GetElementalDamage(out ElementType element, 1);
                break;

            //elemental resist
            case StatType.IceResistance:
                value = playerStats.GetElementalResistance(ElementType.Ice) * 100;
                break;
            case StatType.FireResistance:
                value = playerStats.GetElementalResistance(ElementType.Fire) * 100;
                break;
            case StatType.LightningResistance:
                value = playerStats.GetElementalResistance(ElementType.Lightning) * 100;
                break;
        }
        statValue.text = IsPercentageStat(statSlotType) ? value + "%" : value.ToString() + "<color=#00000000>.</color>";
    }

    private bool IsPercentageStat(StatType type)
    {
        switch (type)
        {
            case StatType.CritChance:
            case StatType.CritPower:
            case StatType.ArmorReduction:
            case StatType.IceResistance:
            case StatType.FireResistance:
            case StatType.LightningResistance:
            case StatType.AttackSpeed:
            case StatType.Evasion:
                return true;
            default:
                return false;
        }
    }

    public string GetStatNameByType(StatType type)
    {
        switch (type)
        {
            case StatType.MaxHealth: return "Max HP";
            case StatType.HealthRegen: return "HP Regeneration per second";
            case StatType.Armor: return "Armor";
            case StatType.Evasion: return "Evasion";

            case StatType.Strength: return "Strength";
            case StatType.Agility: return "Agility";
            case StatType.Intellegence: return "Intellegence";
            case StatType.Vitality: return "Vitality";

            case StatType.AttackSpeed: return "Attack Speed";
            case StatType.Damage: return "Physical Damage";
            case StatType.CritChance: return "Critical Chance";
            case StatType.CritPower: return "Critical Power";
            case StatType.ArmorReduction: return "Armor Reduction";

            case StatType.FireDamage: return "Magic Fire Damage";
            case StatType.IceDamage: return "Magic Ice Damage";
            case StatType.LightningDamage: return "Magic Lightning Damage";
            case StatType.ElementalDamage: return "Elementatal Damage";

            case StatType.IceResistance: return "Ice Resistance";
            case StatType.FireResistance: return "Fire Resistance";
            case StatType.LightningResistance: return "Lightning Resistance";

            default: return "Unknown Stat";

        }
    }
}

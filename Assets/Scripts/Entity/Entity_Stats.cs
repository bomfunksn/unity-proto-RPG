using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHp;
    public Stat_MajorGroup major;
    public Stat_OffenceGroup offence;
    public Stat_DefenceGroup defence;


    public float GetMaxHealth()
    {
        float baseHp = maxHp.GetValue();
        float bonusHp = major.vitality.GetValue() * 5;

        return baseHp + bonusHp;

    }

    public float GetEvasion()
    {
        float baseEvasion = defence.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * .5f;

        float evasionCap = 85;

        return baseEvasion + bonusEvasion;
    }

}

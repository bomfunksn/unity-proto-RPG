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

}

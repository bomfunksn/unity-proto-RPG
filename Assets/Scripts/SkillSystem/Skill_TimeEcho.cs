using UnityEngine;

public class Skill_TimeEcho : Skill_Base
{
    [SerializeField] private GameObject timeEchoPrefab;
    [SerializeField] private float timeEchoDuration;

    [Header("Attack Upgrades")]
    [SerializeField] private int maxAttacks = 3;
    [SerializeField] private float duplicateChance = .3f;


public float GetDuplicateChance()
    {
        if (upgradeType != SkillUpgradeType.TimeEcho_DuplicateChance)
            return 0;

        return duplicateChance;
    }

    public int GetMaxAttacks()
    {
        if (upgradeType == SkillUpgradeType.TimeEcho_SingleAttack || upgradeType == SkillUpgradeType.TimeEcho_DuplicateChance)
            return 1;

        if (upgradeType == SkillUpgradeType.TimeEcho_MultiAttack)
            return maxAttacks;
        return 0;
    }

    public float GetEchoDuration()
    {
        return timeEchoDuration;
    }
    

    public override void TryUseSkill()
    {
        if (CanUseSkill() == false)
            return;

        CreateTimeEcho();
    }

    public void CreateTimeEcho(Vector3? targetPosition = null) //? vector3 ......... = null - это нулл по умолчанию
    {
        Vector3 position = targetPosition ?? transform.position;

        GameObject timeEcho = Instantiate(timeEchoPrefab, position, Quaternion.identity);
        timeEcho.GetComponent<SkillObject_TimeEcho>().SetupEcho(this);

    }
}

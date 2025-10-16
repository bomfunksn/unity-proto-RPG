using UnityEngine;

public class Skill_Chronosphere : Skill_Base
{
    [SerializeField] private GameObject chronospherePrefab;

    [Header("Slowing down percent")]
    [SerializeField] private float slowDownPercent = .8f;
    [SerializeField] private float slowDownChronosphereDuration = 5;

    [Header("Spell casting upgrade")]
    [SerializeField] private float spellCastingChronosphereSlowDown = 1;
    [SerializeField] private float spellCastingChronosphereDuration = 8;

    [Header("Chronosphere detail")]
    public float maxChronosphereSize = 10;
    public float expandSpeed = 3;

    public float GetChronosphereDuration()
    {
        if (upgradeType == SkillUpgradeType.Chronosphere_SlowingDown)
            return slowDownChronosphereDuration;
        else
            return spellCastingChronosphereDuration;
    }

    public float GetSlowPercrntage()
    {
        if (upgradeType == SkillUpgradeType.Chronosphere_SlowingDown)
            return slowDownPercent;
        else
            return spellCastingChronosphereSlowDown;
    }
    

    public bool InstantChronosphere()
    {
        return upgradeType != SkillUpgradeType.Chronosphere_EchoSpam
        && upgradeType != SkillUpgradeType.Chronosphere_ShardSpam;
    }
    public void CreateChronosphere()
    {
        GameObject chronosphere = Instantiate(chronospherePrefab, transform.position, Quaternion.identity);
        chronosphere.GetComponent<SkillObject_Chronosphere>().SetupChronosphere(this);
    }
    
}

using System.Collections.Generic;
using UnityEngine;

public class Skill_Chronosphere : Skill_Base
{
    [SerializeField] private GameObject chronospherePrefab;

    [Header("Slowing down percent")]
    [SerializeField] private float slowDownPercent = .8f;
    [SerializeField] private float slowDownChronosphereDuration = 5;

    [Header("Shard casting upgrade")]
    [SerializeField] private int shardsToCast = 10;
    [SerializeField] private float shardCastingChronosphereSlow = 1;
    [SerializeField] private float shardCastingChronosphereDuration = 8;
    private float spellCastTimer;
    private float spellsPerSecond;

    [Header("Time echo casting upgrade")]
    [SerializeField] private int echoToCast = 8;
    [SerializeField] private float echoCastingChronosphereSlow = 1;
    [SerializeField] private float echoCastingChronosphereDuration = 6;
    [SerializeField] private float healthToRestoreWithEcho = .05f;

    [Header("Chronosphere detail")]
    public float maxChronosphereSize = 10;
    public float expandSpeed = 3;

    private List<Enemy> trappedTargets = new List<Enemy>();
    private Transform currentTarget;

    public void CreateSphere()
    {
        spellsPerSecond = GetSpellsToCast() / GetSphereDuration();

        GameObject chronosphere = Instantiate(chronospherePrefab, transform.position, Quaternion.identity);
        chronosphere.GetComponent<SkillObject_Chronosphere>().SetupSphere(this);
    }

    public void DoSpellCasting()
    {
        spellCastTimer -= Time.deltaTime;

        if (currentTarget == null)
            currentTarget = FindTargetInSphere();

        if (currentTarget != null && spellCastTimer < 0)
        {
            CastSpell(currentTarget);
            spellCastTimer = 1 / spellsPerSecond;
            currentTarget = null;
        }
    }
    
    private void CastSpell(Transform target)
    {
        if (upgradeType == SkillUpgradeType.Chronosphere_EchoSpam)
        {

            Vector3 offset = Random.value < .5f ? new Vector2(1, 0) : new Vector2(-1, 0);
            skillManager.timeEcho.CreateTimeEcho(target.position + offset);
        }
        
        if(upgradeType == SkillUpgradeType.Chronosphere_ShardSpam)
        {
            skillManager.shard.CreateRawShard(target, true);
        }
    }

    private Transform FindTargetInSphere()
    {
        trappedTargets.RemoveAll(target => target == null || target.health.isDead);

        if (trappedTargets.Count == 0)
            return null;

        int randomIndex = Random.Range(0, trappedTargets.Count);
        return trappedTargets[randomIndex].transform;
    }


    public float GetSphereDuration()
    {
        if (upgradeType == SkillUpgradeType.Chronosphere_SlowingDown)
            return slowDownChronosphereDuration;
        else if (upgradeType == SkillUpgradeType.Chronosphere_ShardSpam)
            return shardCastingChronosphereDuration;
        else if (upgradeType == SkillUpgradeType.Chronosphere_EchoSpam)
            return echoCastingChronosphereDuration;

        return 0;
    }

    public float GetSlowPercrntage()
    {
        if (upgradeType == SkillUpgradeType.Chronosphere_SlowingDown)
            return slowDownPercent;
        else if (upgradeType == SkillUpgradeType.Chronosphere_ShardSpam)
            return shardCastingChronosphereSlow;
        else if (upgradeType == SkillUpgradeType.Chronosphere_EchoSpam)
            return echoCastingChronosphereSlow;

        return 0;
    }

    private int GetSpellsToCast()
    {
        if (upgradeType == SkillUpgradeType.Chronosphere_ShardSpam)
            return shardsToCast;
        else if (upgradeType == SkillUpgradeType.Chronosphere_EchoSpam)
            return echoToCast;

        return 0;
    } 
    

    public bool InstantSphere()
    {
        return upgradeType != SkillUpgradeType.Chronosphere_EchoSpam
        && upgradeType != SkillUpgradeType.Chronosphere_ShardSpam;
    }

    public void AddTarget(Enemy targetToAdd)
    {
        trappedTargets.Add(targetToAdd);
    }

    public void ClearTargets()
    {

        foreach (var enemy in trappedTargets)
            enemy.StopSlowDown();

        trappedTargets = new List<Enemy>();
    }
    
}

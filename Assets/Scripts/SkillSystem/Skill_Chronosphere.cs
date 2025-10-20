using System.Collections.Generic;
using UnityEngine;

public class Skill_Chronosphere : Skill_Base
{
    [SerializeField] private GameObject chronospherePrefab;

    [Header("Slowing down percent")]
    [SerializeField] private float slowDownPercent = .8f;
    [SerializeField] private float slowDownChronosphereDuration = 5;

    [Header("Spell casting upgrade")]
    [SerializeField] private int spellsToCast = 10;
    [SerializeField] private float spellCastingChronosphereSlowDown = 1;
    [SerializeField] private float spellCastingChronosphereDuration = 8;
    private float spellCastTimer;
    private float spellsPerSecond;

    [Header("Chronosphere detail")]
    public float maxChronosphereSize = 10;
    public float expandSpeed = 3;

    private List<Enemy> trappedTargets = new List<Enemy>();
    private Transform currentTarget;

    public void CreateSphere()
    {
        spellsPerSecond = spellsToCast / GetSphereDuration();

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
        if (trappedTargets.Count == 0)
            return null;
        
        int randomIndex = Random.Range(0, trappedTargets.Count);
        Transform target = trappedTargets[randomIndex].transform;

        if(target == null)
        {
            trappedTargets.RemoveAt(randomIndex);
            return null;
        }
        return target;
    }


    public float GetSphereDuration()
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

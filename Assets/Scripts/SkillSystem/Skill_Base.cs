using UnityEngine;

public class Skill_Base : MonoBehaviour
{
    [Header("General Details")]

    [SerializeField] private float cooldown;
    private float lastTimeUsed;

    protected virtual void Awake()
    {
        lastTimeUsed = lastTimeUsed - cooldown;
    }


    public bool CanUseSkill()
    {
        if (OnCooldown())
        {
            Debug.Log("dash is on cooldown");
            return false;
        }

        return true;
    }

    private bool OnCooldown() => Time.time < lastTimeUsed + cooldown;
    public float SetSkillOnCooldown() => lastTimeUsed = Time.time;
    public void ResetCooldownBy(float cooldownReduction) => lastTimeUsed = lastTimeUsed + cooldownReduction;
    public void ResetCooldown() => lastTimeUsed = Time.time;
}

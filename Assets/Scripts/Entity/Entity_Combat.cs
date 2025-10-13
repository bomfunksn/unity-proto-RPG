using NUnit.Framework;
using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX vfx;
    private Entity_Stats stats;
    public DamageScaleData basicAttackScale;

    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targerCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;

    [Header("status effect details")]
    [SerializeField] private float defaultDuration = 3;
    [SerializeField] private float chillSlowMultiplier = .2f;
    [SerializeField] private float electrifyChargeBuildUp = .4f;
    [Space]
    [SerializeField] private float fireScale = .8f;
    [SerializeField] private float lightningScale = 2.5f;

    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
    }

    public void PerformAttack()
    {
        foreach (var target in GetDetectedColliders())
        {
            IDamageble damageble = target.GetComponent<IDamageble>();

            if (damageble == null)
                continue;

            ElementalEffectData effectData = new ElementalEffectData(stats, basicAttackScale);

            float elementalDamage = stats.GetElementalDamage(out ElementType element, .6f);
            float damage = stats.GetPhysicalDamage(out bool isCrit);


            bool targetGotHit = damageble.TakeDamage(damage, elementalDamage, element, transform);

            if (element != ElementType.none)
                target.GetComponent<Entity_StatusHandler>().ApplyStatusEffect(element, effectData);

            if (targetGotHit)
            {
                vfx.UpdateOnHitColor(element);
                vfx.CreateOnHitVFX(target.transform, isCrit);
            }
        }
    }

    protected Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targerCheckRadius, whatIsTarget);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targerCheckRadius);
    }
}

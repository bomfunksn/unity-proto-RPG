using Unity.Mathematics;
using UnityEngine;

public class SkillObject_Base : MonoBehaviour
{
    [SerializeField] private GameObject onHitVfx;

    [Space]
    [SerializeField] protected LayerMask whatIsEnemy;
    [SerializeField] protected Transform targetcheck;
    [SerializeField] protected float checkRadius = 1;

    protected Animator anim;
    protected Entity_Stats playerStats;
    protected DamageScaleData damageScaleData;
    protected ElementType usedElement;
    protected bool targetGotHit;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    protected void DamageEnemiesInRadius(Transform t, float radius)
    {
        foreach (var target in GetEnemiesAround(t, radius))
        {
            IDamageble damageble = target.GetComponent<IDamageble>();

            if (damageble == null)
                continue;

            AttackData attackData = playerStats.GetAttackData(damageScaleData);
            Entity_StatusHandler statusHandler = target.GetComponent<Entity_StatusHandler>();

            float physDamage = attackData.physicalDamage;
            float elemeDamage = attackData.elementalDamage;
            ElementType element = attackData.element;

            targetGotHit = damageble.TakeDamage(physDamage, elemeDamage, element, transform);

            if (element != ElementType.none)
                target.GetComponent<Entity_StatusHandler>().ApplyStatusEffect(element, attackData.effectData);

            if (targetGotHit)
                Instantiate(onHitVfx, target.transform.position, Quaternion.identity);

            usedElement = element;
        }
    }

    protected Transform FindClosestTarget()
    {
        Transform target = null;
        float closestDistance = Mathf.Infinity;

        foreach (var enemy in GetEnemiesAround(transform, 8))
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                target = enemy.transform;
                closestDistance = distance;
            }
        }
        return target;
    }

    protected Collider2D[] GetEnemiesAround(Transform t, float radius)
    {
        return Physics2D.OverlapCircleAll(t.position, radius, whatIsEnemy);
    }

    protected virtual void OnDrawGizmos()
    {
        if (targetcheck == null)
            targetcheck = transform;

        Gizmos.DrawWireSphere(targetcheck.position, checkRadius);
    }
}
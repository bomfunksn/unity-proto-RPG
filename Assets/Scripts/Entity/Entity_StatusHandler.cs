using System.Collections;
using UnityEngine;

public class Entity_StatusHandler : MonoBehaviour
{
    private Entity entity;
    private Entity_VFX entityVfx;
    private Entity_Stats entityStats;
    private Entity_Health entityHealth;
    private ElementType currentEffect = ElementType.none;

    [Header("electrify effect details")]
    [SerializeField] private GameObject lightningStrikeVfx;
    [SerializeField] private float currentCharge;
    [SerializeField] private float maximumCharge = 1f;
    private Coroutine shockCo;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        entityVfx = GetComponent<Entity_VFX>();
        entityStats = GetComponent<Entity_Stats>();
        entityHealth = GetComponent<Entity_Health>();
    }

    public void RemoveAllNegativeEffects()
    {
        StopAllCoroutines();
        currentEffect = ElementType.none;
        entityVfx.StopAllVfx();
    }

    public void ApplyStatusEffect(ElementType element, ElementalEffectData effectData)
    {
        if (element == ElementType.Ice && CanBeApplied(ElementType.Ice))
            ApplyChillEffect(effectData.chillDuration, effectData.chillSlowMultiplier);
        if (element == ElementType.Fire && CanBeApplied(ElementType.Fire))
            ApplyBurnEffect(effectData.totalBurnDuration, effectData.totalBurnDamage);
        if (element == ElementType.Lightning && CanBeApplied(ElementType.Lightning))
            ApplyShockEffect(effectData.shockDuration, effectData.shockDamage,effectData.shockCharge);
    }

    private void ApplyShockEffect(float duration, float damage, float charge)
    {
        float lightningResistance = entityStats.GetElementalResistance(ElementType.Lightning);
        float finalcharge = charge * (1 - lightningResistance);
        currentCharge = currentCharge + finalcharge;

        if (currentCharge >= maximumCharge)
        {
            DoShockStrike(damage);
            StopShockEffect();
            return;
        }
        if (shockCo != null)
            StopCoroutine(shockCo);

        shockCo = StartCoroutine(ShockEffectCo(duration));

    }

    private void StopShockEffect()
    {
        currentEffect = ElementType.none;
        currentCharge = 0;
        entityVfx.StopAllVfx();
    }

    private void DoShockStrike(float damage)
    {
        Instantiate(lightningStrikeVfx, transform.position, Quaternion.identity);
        entityHealth.ReduceHealth(damage);
    }

    private IEnumerator ShockEffectCo(float duration)
    {
        currentEffect = ElementType.Lightning;
        entityVfx.PlayOnStatusVfx(duration, ElementType.Lightning);

        yield return new WaitForSeconds(duration);
        StopShockEffect();
    }

    private void ApplyBurnEffect(float duration, float fireDamage)
    {
        float fireResistance = entityStats.GetElementalResistance(ElementType.Fire);
        float finalDamage = fireDamage * (1 - fireResistance);

        StartCoroutine(BurnEffectCo(duration, finalDamage));
    }

    private IEnumerator BurnEffectCo(float duration, float totalDamage)
    {
        currentEffect = ElementType.Fire;
        entityVfx.PlayOnStatusVfx(duration, ElementType.Fire);

        int ticksPerSecond = 2;
        int tickCount = Mathf.RoundToInt(ticksPerSecond * duration);

        float damagePerTick = totalDamage / tickCount;
        float tickInterval = 1f / ticksPerSecond;

        for (int i = 0; i < tickCount; i++)
        {
            entityHealth.ReduceHealth(damagePerTick);
            yield return new WaitForSeconds(tickInterval);
        }

        currentEffect = ElementType.none;
    }

    private void ApplyChillEffect(float duration, float slowMultiplier)
    {
        float iceResistance = entityStats.GetElementalResistance(ElementType.Ice);
        float finalDuration = duration * (1 - iceResistance);

        StartCoroutine(ChillEffectCo(finalDuration, slowMultiplier));
    }

    private IEnumerator ChillEffectCo(float duration, float slowMultiplier)
    {
        entity.SlowDownEntity(duration, slowMultiplier);
        currentEffect = ElementType.Ice;
        entityVfx.PlayOnStatusVfx(duration, ElementType.Ice);
        yield return new WaitForSeconds(duration);
        currentEffect = ElementType.none;
    }

    public bool CanBeApplied(ElementType element)
    {
        if (element == ElementType.Lightning && currentEffect == ElementType.Lightning)
            return true;

        return currentEffect == ElementType.none;
    }

}

using UnityEngine;

public interface IDamageble
{
    public bool TakeDamage(float damage, float elementalDamage, Transform damageDealer);
}

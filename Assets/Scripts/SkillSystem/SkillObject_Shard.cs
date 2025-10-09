using System;
using Unity.Mathematics;
using UnityEngine;

public class SkillObject_Shard : SkillObject_Base
{

    [SerializeField] private GameObject vfxPrefab;

public void SetupShard(float detonationTime)
        {
        Invoke(nameof(Explode), detonationTime);
    }

    private void Explode()
    {
        DamageEnemiesInRadius(transform, checkRadius);
        Instantiate(vfxPrefab, transform.position, quaternion.identity);

        Destroy(gameObject);
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() == null)
            return;

        Explode();
    }
}

using System;
using Unity.Mathematics;
using UnityEngine;

public class SkillObject_Shard : SkillObject_Base
{

    public event Action OnExplode;
    private Skill_Shard shardManager;

    [SerializeField] private GameObject vfxPrefab;
    private Transform target;
    private float speed;

    private void Update()
    {
        if (target == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

    }

    public void MoveTowardsClosestTarget(float speed)
    {
        target = FindClosestTarget();
        this.speed = speed;

    }

    public void SetupShard(Skill_Shard shardManager)
    {
        this.shardManager = shardManager;

        playerStats = shardManager.player.stats;
        damageScaleData = shardManager.damageScaleData;

        float detonationTime = shardManager.GetDetonateTime();

        Invoke(nameof(Explode), detonationTime);
    }

    public void SetupShard(Skill_Shard shardManager, float detonationTime, bool canMove, float shardSpeed)
    {
        this.shardManager = shardManager;

        playerStats = shardManager.player.stats;
        damageScaleData = shardManager.damageScaleData;

        Invoke(nameof(Explode), detonationTime);

        if (canMove)
            MoveTowardsClosestTarget(shardSpeed);
    }

    public void Explode()
    {
        DamageEnemiesInRadius(transform, checkRadius);
        // SpriteRenderer sprite = Instantiate(vfxPrefab, transform.position, quaternion.identity).GetComponentInChildren<SpriteRenderer>(); - так в 1 строку
        GameObject vfx = Instantiate(vfxPrefab, transform.position, quaternion.identity);
        vfx.GetComponentInChildren<SpriteRenderer>().color = shardManager.player.vfx.GetEementColor(usedElement);

        OnExplode?.Invoke();
        Destroy(gameObject);
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() == null)
            return;

        Explode();
    }
}
using Unity.Mathematics;
using UnityEngine;

public class Skill_Shard : Skill_Base
{
    [SerializeField] private GameObject shardPrefab;
    [SerializeField] private float detonateTime = 2;


    public void CreateShard()
    {
        if (upgradeType == SkillUpgradeType.None)
            return;
            
        GameObject shard = Instantiate(shardPrefab, transform.position, quaternion.identity);
        shard.GetComponent<SkillObject_Shard>().SetupShard(detonateTime);
    }
}

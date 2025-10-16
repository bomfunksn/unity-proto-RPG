using UnityEngine;

public enum SkillUpgradeType
{
    None,
    Dash,
    Dash_ClonOnStart,
    Dash_ClonOnStartAndArrival,
    Dash_ShardOnStart,
    Dash_ShardOnStartAndArrival,

    Shard,
    Shard_MoveToEnemy,
    Shard_Multicast,
    Shard_Teleport,
    Shard_TeleportHpRewind,

    SwordThrow,
    SwordThrow_Spin,
    SwordThrow_Pierce,
    SwordThrow_Bounce,

    TimeEcho,
    TimeEcho_SingleAttack,
    TimeEcho_MultiAttack,
    TimeEcho_DuplicateChance,

    TimeEcho_HealWisp,

    TimeEcho_CleanseWisp,
    TimeEcho_CooldownWisp,

    Chronosphere_SlowingDown,
    Chronosphere_EchoSpam,
Chronosphere_ShardSpam
}

using UnityEngine;

public class Player_BasicAtackState : EntityState
{
    private float atackVelocityTimer;
    public Player_BasicAtackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();


        GenerateAtackVelocity();
    }
    public override void Update()
    {
        base.Update();
        HandleAtackVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
    private void HandleAtackVelocity()
    {
        atackVelocityTimer -= Time.deltaTime;

        if (atackVelocityTimer < 0)
            player.SetVelocity(0, rb.linearVelocity.y);
    }

    private void GenerateAtackVelocity()
    {
        atackVelocityTimer = player.atackVelocityDuration;
        player.SetVelocity(player.atackVelocity.x * player.facingDir, player.atackVelocity.y);
    }
}

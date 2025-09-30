using UnityEngine;

public class Player_Deadstate : PlayerState
{
    public Player_Deadstate(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        input.Disable();
        rb.simulated = false;
    }
}

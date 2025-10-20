using UnityEngine;

public class Player_ChronosphereState : PlayerState
{

    private Vector2 originalPosition;
    private float originalGravity;
    private float maxDistanceToGoUp;

    private bool isLevitating;
    private bool createdChronosphere;

    public Player_ChronosphereState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        originalPosition = player.transform.position;
        originalGravity = rb.gravityScale;
        maxDistanceToGoUp = GetAvailableRiseDistance();

        player.SetVelocity(0, player.riseSpeed);
    }

    public override void Update()
    {
        base.Update();

        if (Vector2.Distance(originalPosition, player.transform.position) >= maxDistanceToGoUp && isLevitating == false)
            Levitate();

            if(isLevitating)
        {
            skillManager.chronosphere.DoSpellCasting();

            if (stateTimer < 0)
            {
                rb.gravityScale = originalGravity;
                isLevitating = false;
                stateMachine.ChangeState(player.idleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        createdChronosphere = false;
    }

    private void Levitate()
    {
        isLevitating = true;
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;

        stateTimer = skillManager.chronosphere.GetSphereDuration();

        if(createdChronosphere ==false)
        {
            createdChronosphere = true;
            skillManager.chronosphere.CreateSphere();
        }
    }
    
    private float GetAvailableRiseDistance()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.up, player.riseMaxDistance, player.whatIsGround);

        return hit.collider != null ? hit.distance - 1 : player.riseMaxDistance;
    }
}

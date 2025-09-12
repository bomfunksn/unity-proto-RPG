using NUnit.Framework;
using UnityEngine;

public class Player_WallSlideState : EntityState
{
    public Player_WallSlideState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        HandleWallSlide(); //ПОЧЕМУ ОНО НЕ ВЫЗЫВАЕТСЯ ДО ПРИЛИПАНИЯ К СТЕНЕ


        if (input.Player.Jump.WasPressedThisFrame())
            stateMachine.ChangeState(player.wallJumpState);

        if (player.wallDetected == false)
            stateMachine.ChangeState(player.fallState);

        if (player.groundDetected)
            {
                stateMachine.ChangeState(player.idleState);

                if(player.facingDir != player.moveInput.x)
                player.Flip();
            }
    }

    private void HandleWallSlide()
    {
        if (player.moveInput.y < 0) //ВОТ ТУТ, если я жму вниз, я лечу вниз, с той же скоростью, всё ок
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y);
        else
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y * player.wallSlideSlowMultiplier); // НО если я не жму вниз, например, при падении, почему я не замедляюсь. Тут же наследование от ентити напрямую.
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/FastLeftAttack2", fileName = "PlayerState_FastLeftAttack2")]
public class PlayerState_FastLeftAttack2 : PlayerState
{
    [SerializeField] float staminaCost;
    [SerializeField] float deceleration = 50;
    [SerializeField] float moveSpeed = 6;
    public override void Enter()
    {
        base.Enter();
        //player.DeductStamina(staminaCost);
        input.HasAttackInputBuffer = 0;
        player.Move(moveSpeed * player.transform.forward);
        currentSpeed = player.MoveSpeed;
        if (player.isLocking)
        {
            player.transform.rotation = Quaternion.Euler(0, Quaternion.LookRotation(player.enemy.position - player.transform.position).eulerAngles.y, 0);
        }
    }
    public override void LogicUpdate()
    {
        if (input.LeftAttack)
        {
            input.SetAttackInputBufferTimer(1);
        }
        if (input.RightAttack)
        {
            input.SetAttackInputBufferTimer(2);
        }
        if (input.Dash && player.HasStamina)
        {
            stateMachine.SwitchState(typeof(PlayerState_Dash));
        }
        if (IsAnimationFinished)
        {
            if (input.HasAttackInputBuffer == 1 && player.HasStamina)
            {
                stateMachine.SwitchState(typeof(PlayerState_FastLeftAttack1));
            }
            else if (input.HasAttackInputBuffer == 2 && player.HasStamina)
            {
                stateMachine.SwitchState(typeof(PlayerState_FastRightAttack));
            }
            else
            {
                stateMachine.SwitchState(typeof(PlayerState_Idle));
            }
        }

        currentSpeed = Vector3.MoveTowards(currentSpeed, Vector3.zero, deceleration * Time.deltaTime);
        animator.SetFloat("speed", currentSpeed.magnitude);
    }
    public override void PhysicUpdate()
    {
        player.Move(currentSpeed);
    }
    public override void Exit()
    {
        player.ClearHitCache();
    }
}

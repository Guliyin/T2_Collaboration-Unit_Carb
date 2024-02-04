using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/RightAttack", fileName = "PlayerState_RightAttack")]
public class PlayerState_LeftAttack : PlayerState
{
    [SerializeField] float deceleration = 50;
    public override void Enter()
    {
        base.Enter();
        currentSpeed = player.MoveSpeed;
        if (player.isLocking)
        {
            player.transform.rotation = Quaternion.Euler(0, Quaternion.LookRotation(player.enemy.position - player.transform.position).eulerAngles.y, 0);
        }
    }
    public override void LogicUpdate()
    {
        if (input.Attack)
        {
            input.SetAttackInputBufferTimer();
        }
        if (input.Dash)
        {
            stateMachine.SwitchState(typeof(PlayerState_Dash));
        }
        if (IsAnimationFinished)
        {
            if (input.HasAttackInputBuffer)
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
}

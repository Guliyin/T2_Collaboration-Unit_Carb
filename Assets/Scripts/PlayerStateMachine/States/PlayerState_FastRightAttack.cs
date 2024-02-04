using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/FastRightAttack", fileName = "PlayerState_FastRightAttack")]
public class PlayerState_FastRightAttack : PlayerState
{
    [SerializeField] float deceleration = 50;
    public override void Enter()
    {
        base.Enter();
        input.HasAttackInputBuffer = 0;
        currentSpeed = player.MoveSpeed;
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
        if (input.Dash)
        {
            stateMachine.SwitchState(typeof(PlayerState_Dash));
        }
        if (IsAnimationFinished)
        {
            if (input.HasAttackInputBuffer==1)
            {
                stateMachine.SwitchState(typeof(PlayerState_FastLeftAttack));
            }
            else if(input.HasAttackInputBuffer == 2)
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

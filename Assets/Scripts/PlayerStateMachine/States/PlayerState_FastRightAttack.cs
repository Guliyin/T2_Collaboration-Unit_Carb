using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/FastRightAttack", fileName = "PlayerState_FastRightAttack")]
public class PlayerState_FastRightAttack : PlayerState
{
    [SerializeField] float deceleration = 50;
    public override void Enter()
    {
        base.Enter();
        input.HasAttackInputBuffer = false;
        currentSpeed = player.MoveSpeed;
    }
    public override void LogicUpdate()
    {
        if (input.Attack)
        {
            input.SetAttackInputBufferTimer();
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

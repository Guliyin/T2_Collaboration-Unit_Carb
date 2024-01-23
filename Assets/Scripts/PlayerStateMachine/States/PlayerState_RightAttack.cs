using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/RightAttack", fileName = "PlayerState_RightAttack")]
public class PlayerState_RightAttack : PlayerState
{
    [SerializeField] float deceleration = 50;
    public override void Enter()
    {
        base.Enter();
        currentSpeed = player.MoveSpeed;
    }
    public override void LogicUpdate()
    {
        if (IsAnimationFinished)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }

        currentSpeed = Vector3.MoveTowards(currentSpeed, Vector3.zero, deceleration * Time.deltaTime);
        animator.SetFloat("speed", currentSpeed.magnitude);
    }
    public override void PhysicUpdate()
    {
        player.Move(currentSpeed);
    }
}

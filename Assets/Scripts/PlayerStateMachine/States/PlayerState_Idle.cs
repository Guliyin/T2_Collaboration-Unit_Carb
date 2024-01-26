using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/Idle", fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayerState
{
    [SerializeField] float deceleration = 5;
    public override void Enter()
    {
        base.Enter();
        currentSpeed = player.MoveSpeed;
    }
    public override void LogicUpdate()
    {
        if (input.Move)
        {
            if (input.Sprint) 
                stateMachine.SwitchState(typeof(PlayerState_Sprint));
            else
                stateMachine.SwitchState(typeof(PlayerState_Move));
        }
        if (input.Dash)
        {
            stateMachine.SwitchState(typeof(PlayerState_Dash));
        }
        if (input.Attack)
        {
            System.Type a = input.RightAttack ? typeof(PlayerState_RightAttack) : typeof(PlayerState_Idle);
            stateMachine.SwitchState(a);
        }

        currentSpeed = Vector3.MoveTowards(currentSpeed, Vector3.zero, deceleration * Time.deltaTime);
        animator.SetFloat("speed", currentSpeed.magnitude);
    }
    public override void PhysicUpdate()
    {
        player.Move(currentSpeed);
    }
}

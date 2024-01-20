using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/Move", fileName = "PlayerState_Move")]
public class PlayerState_Move : PlayerState
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float acceleration = 5f;
    public override void Enter()
    {
        base.Enter();
        currentSpeed = player.MoveSpeed;
    }
    public override void LogicUpdate()
    {
        if (!input.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        currentSpeed = Vector3.MoveTowards(currentSpeed, input.axes * runSpeed, acceleration * Time.deltaTime);
    }
    public override void PhysicUpdate()
    {
        player.Move(currentSpeed);
    }
}

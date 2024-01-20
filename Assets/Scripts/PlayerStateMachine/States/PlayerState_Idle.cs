using UnityEngine;

[CreateAssetMenu(menuName ="States/Player/Idle", fileName ="PlayerState_Idle")]
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
            stateMachine.SwitchState(typeof(PlayerState_Move));
        }
        currentSpeed = Vector3.MoveTowards(currentSpeed, Vector3.zero, deceleration * Time.deltaTime);
    }
    public override void PhysicUpdate()
    {
        player.Move(currentSpeed);
    }
}

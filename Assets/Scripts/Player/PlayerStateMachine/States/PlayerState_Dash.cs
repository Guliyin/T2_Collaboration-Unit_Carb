using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/Dash", fileName = "PlayerState_Dash")]
public class PlayerState_Dash : PlayerState
{
    [SerializeField] float staminaCost;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float dashSpeed;
    Vector3 dir;
    public override void Enter()
    {
        base.Enter();
        player.DeductStamina(staminaCost);
        currentSpeed = player.MoveSpeed;

        if (input.moveAxes != Vector3.zero)
        {
            float dirQ = Quaternion.LookRotation(input.moveAxes).eulerAngles.y + player.cam.transform.rotation.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, dirQ, 0);
            dir = (rotation * Vector3.forward).normalized;
        }
        else
        {
            dir = player.transform.forward;
        }

    }
    public override void LogicUpdate()
    {
        if (IsAnimationFinished)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
    }
    public override void PhysicUpdate()
    {
        player.Move(dir * dashSpeed);
    }
    public override void Exit()
    {
        player.Move(currentSpeed);
    }
}

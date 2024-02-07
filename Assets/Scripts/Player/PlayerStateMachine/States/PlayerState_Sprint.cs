using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/Sprint", fileName = "PlayerState_Sprint")]

public class PlayerState_Sprint : PlayerState
{
    [SerializeField] float staminaCost;
    [SerializeField] float sprintSpeed = 7f;
    [SerializeField] float turnRate = 10;
    [SerializeField] float acceleration = 5f;

    public override void Enter()
    {
        base.Enter();
        currentSpeed = player.MoveSpeed;
        player.DustAnim(true);
    }
    public override void LogicUpdate()
    {
        float targetRot = 0;
        player.DeductStamina(staminaCost*Time.deltaTime);

        if (input.LeftAttack && player.HasStamina)
        {
            stateMachine.SwitchState(typeof(PlayerState_LeftAttack));
        }
        if (input.RightAttack && player.HasStamina)
        {
            stateMachine.SwitchState(typeof(PlayerState_RightAttack));
        }

        if (input.Dash && player.HasStamina)
        {
            stateMachine.SwitchState(typeof(PlayerState_Dash));
        }

        if (!player.HasStamina || (!input.Sprint && input.Move))
        {
            stateMachine.SwitchState(typeof(PlayerState_Move));
        }

        if (!input.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        else
        {
            targetRot = Quaternion.LookRotation(input.moveAxes).eulerAngles.y + player.cam.transform.rotation.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetRot, 0);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rotation, turnRate * Time.deltaTime);
        }


        Vector3 targetDir = Quaternion.Euler(0, targetRot, 0) * Vector3.forward;
        currentSpeed = Vector3.MoveTowards(currentSpeed, targetDir.normalized * sprintSpeed, acceleration * Time.deltaTime);
        animator.SetFloat("speed", currentSpeed.magnitude);
    }
    public override void PhysicUpdate()
    {
        player.Move(currentSpeed);
    }
    public override void Exit()
    {
        player.DustAnim(false);
    }
}

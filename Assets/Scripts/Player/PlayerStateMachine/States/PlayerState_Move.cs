using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/Move", fileName = "PlayerState_Move")]
public class PlayerState_Move : PlayerState
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float turnRate = 20f;
    [SerializeField] float acceleration = 5f;
    public override void Enter()
    {
        base.Enter();
        currentSpeed = player.MoveSpeed;
        player.LegMoving = true;
    }
    public override void LogicUpdate()
    {
        float targetRot = 0;

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

        if (!input.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        else if (input.Move && input.Sprint && player.HasStamina)
        {
            stateMachine.SwitchState(typeof(PlayerState_Sprint));
        }
        else
        {
            targetRot = Quaternion.LookRotation(input.moveAxes).eulerAngles.y + player.cam.transform.rotation.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetRot, 0);
            if (player.isLocking)
            {
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(player.enemy.position - player.transform.position), turnRate * Time.deltaTime);
            }
            else
            {
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rotation, turnRate * Time.deltaTime);
            }
        }

        Vector3 targetDir = Quaternion.Euler(0, targetRot, 0) * Vector3.forward;
        currentSpeed = Vector3.MoveTowards(currentSpeed, targetDir.normalized * runSpeed, acceleration * Time.deltaTime);
        animator.SetFloat("speed", currentSpeed.magnitude);
    }
    public override void PhysicUpdate()
    {
        player.Move(currentSpeed);
    }
    public override void Exit()
    {
        player.LegMoving = false;
    }
}

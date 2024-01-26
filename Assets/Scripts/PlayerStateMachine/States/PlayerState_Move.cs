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
    }
    public override void LogicUpdate()
    {
        float targetRot = 0;

        if (input.Attack)
        {
            System.Type a = input.RightAttack ? typeof(PlayerState_RightAttack) : typeof(PlayerState_Idle);
            stateMachine.SwitchState(a);
        }

        if (input.Dash)
        {
            stateMachine.SwitchState(typeof(PlayerState_Dash));
        }

        if (!input.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        else if (input.Move && input.Sprint)
        {
            stateMachine.SwitchState(typeof(PlayerState_Sprint));
        }
        else
        {
            targetRot = Quaternion.LookRotation(input.moveAxes).eulerAngles.y + player.cam.transform.rotation.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetRot, 0);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rotation, turnRate * Time.deltaTime);
        }

        Vector3 targetDir = Quaternion.Euler(0, targetRot, 0) * Vector3.forward;
        currentSpeed = Vector3.MoveTowards(currentSpeed, targetDir.normalized * runSpeed, acceleration * Time.deltaTime);
        animator.SetFloat("speed", currentSpeed.magnitude);
    }
    public override void PhysicUpdate()
    {
        player.Move(currentSpeed);
    }
}

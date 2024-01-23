using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/Sprint", fileName = "PlayerState_Sprint")]

public class PlayerState_Sprint : PlayerState
{
    [SerializeField] float sprintSpeed = 7f;
    [SerializeField] float turnRate = 10;
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

        if (!input.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        else if (!input.Sprint && input.Move)
        {
            stateMachine.SwitchState(typeof(PlayerState_Move));
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
}

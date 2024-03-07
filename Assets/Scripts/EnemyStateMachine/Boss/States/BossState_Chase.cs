using UnityEngine;

public class BossState_Chase : BossState
{
    public BossState_Chase(string stateName, BossController manager, Animator animator, BossStateMachine stateMachine) : base(stateName, manager, animator, stateMachine) { }

    float timer = 0;

    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            boss.Move(parameters.MoveSpeed, boss.player.position);
        }

        Vector3 dir = boss.player.position - boss.transform.position;
        if (dir.magnitude > parameters.ChargeDistance)
        {
            stateMachine.SwitchState(typeof(BossState_Charge));
        }
        if (dir.magnitude <= parameters.AttackDistance)
        {
            boss.Move(false);
            stateMachine.SwitchState(boss.NextMove());
        }
        animator.SetFloat("Speed", boss.agentCurSpeed / boss.agentMaxSpeed);
    }
}

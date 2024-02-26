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
            boss.Move(boss.player.position);
        }
        if (Vector3.Distance(boss.player.position, boss.transform.position) <= parameters.AttackDistance)
        {
            boss.Move(false);
            stateMachine.SwitchState(boss.NextMove());
        }
        animator.SetFloat("Speed", boss.agentCurSpeed / boss.agentMaxSpeed);
    }
}

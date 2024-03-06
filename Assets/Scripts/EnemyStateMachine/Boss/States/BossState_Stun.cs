using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState_Stun : BossState
{
    public BossState_Stun(string stateName, BossController manager, Animator animator, BossStateMachine stateMachine) : base(stateName, manager, animator, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        if (IsAnimationFinished)
        {
            Debug.Log("FINISH");
            Vector3 dir = boss.player.position - boss.transform.position;
            if (dir.magnitude > 35)
            {
                stateMachine.SwitchState(typeof(BossState_Charge));
            }
            if (dir.magnitude <= parameters.AttackDistance)
            {
                boss.Move(false);
                stateMachine.SwitchState(boss.NextMove());
            }
            stateMachine.SwitchState(typeof(BossState_Chase));
        }
    }
}

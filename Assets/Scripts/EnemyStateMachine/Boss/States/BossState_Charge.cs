using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState_Charge : BossState
{
    public BossState_Charge(string stateName, BossController manager, Animator animator, BossStateMachine stateMachine) : base(stateName, manager, animator, stateMachine) { }

    Vector3 destination;
    public override void Enter()
    {
        base.Enter();
        Vector3 chargeDir = boss.player.position - boss.transform.position;
        destination = boss.transform.position + chargeDir + chargeDir.normalized * 15;
        boss.Move(parameters.CharageSpeed, destination);
    }
    public override void LogicUpdate()
    {
        Vector3 dir = destination - boss.transform.position;
        if (dir.magnitude <= 5 || StateDuration >= parameters.ChargeMaxTime)
        {
            boss.Move(false);
            stateMachine.SwitchState(typeof(BossState_Stun));
        }
        animator.SetFloat("Charge", boss.agentCurSpeed / boss.agentMaxSpeed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState_TreadPre : BossState
{
    public BossState_TreadPre(string stateName, BossController manager, Animator animator, BossStateMachine stateMachine) : base(stateName, manager, animator, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        boss.treadArea.gameObject.SetActive(true);
    }
    public override void LogicUpdate()
    {
        Vector3 direction = boss.player.position - boss.transform.position;
        float theta = Vector3.Angle(boss.transform.forward, boss.treadArea.position - boss.transform.position);

        float yAxisBeforeCorrection = Quaternion.LookRotation(direction, Vector3.up).eulerAngles.y;

        float yAxis = yAxisBeforeCorrection - theta;
        Quaternion finalRot = Quaternion.Euler(0, yAxis, 0);

        boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, finalRot, Time.deltaTime * parameters.TurnRate);


        if (IsAnimationFinished)
        {
            stateMachine.SwitchState(typeof(BossState_Tread));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState_Tread : BossState
{
    public BossState_Tread(string stateName, BossController manager, Animator animator, BossStateMachine stateMachine) : base(stateName, manager, animator, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Tread");
    }
    public override void LogicUpdate()
    {
        if (IsAnimationFinished)
        {
            stateMachine.SwitchState(typeof(BossState_Chase));
        }
    }
}

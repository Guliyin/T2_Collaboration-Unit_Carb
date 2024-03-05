using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState_Summon : BossState
{
    public BossState_Summon(string stateName, BossController manager, Animator animator, BossStateMachine stateMachine) : base(stateName, manager, animator, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        if (IsAnimationFinished)
        {
            stateMachine.SwitchState(typeof(BossState_Chase));
        }

    }
}

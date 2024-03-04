using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionState_Born : MinionState
{
    public MinionState_Born(string stateName, MinionController manager, Animator animator, MinionStateMachine stateMachine) : base(stateName, manager, animator, stateMachine) { }
    public override void Enter()
    {
        transitionDuration = 0;
        base.Enter();
    }
    public override void LogicUpdate()
    {
        if (IsAnimationFinished)
        {
            stateMachine.SwitchState(typeof(MinionState_Chase));
        }
    }
}

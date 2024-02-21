using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionState_AfterAttack : MinionState
{
    public MinionState_AfterAttack(string stateName, MinionController manager, Animator animator, MinionStateMachine stateMachine) : base(stateName, manager, animator, stateMachine) { }
    public override void Enter()
    {
        //transitionDuration = 0f;
        base.Enter();
    }
    public override void LogicUpdate()
    {
        if (IsAnimationFinished)
        {
            stateMachine.SwitchState(typeof(MinionState_Chase));
        }
    }
    //public override void Exit()
    //{
    //    transitionDuration = 0.1f;
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionState_PrepareAttack : MinionState
{
    public MinionState_PrepareAttack(string stateName, MinionController manager, Animator animator, MinionStateMachine stateMachine) : base(stateName, manager, animator, stateMachine) { }
    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        if (IsAnimationFinished)
        {
            stateMachine.SwitchState(typeof(MinionState_Attack));
        }
    }
}

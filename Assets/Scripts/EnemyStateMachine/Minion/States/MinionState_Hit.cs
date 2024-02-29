using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionState_Hit : MinionState
{
    public MinionState_Hit(string stateName, MinionController manager, Animator animator, MinionStateMachine stateMachine) : base(stateName, manager, animator, stateMachine) { }
    public override void Enter()
    {
        base.Enter();
        minion.isHit = true;
        minion.transform.rotation = Quaternion.LookRotation(minion.player.position - minion.transform.position);
    }
    public override void LogicUpdate()
    {
        if (IsAnimationFinished)
        {
            stateMachine.SwitchState(typeof(MinionState_Chase));
        }
    }
    public override void Exit()
    {
        minion.isHit = false;
    }
}

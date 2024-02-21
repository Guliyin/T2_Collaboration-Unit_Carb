using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionState_Dead : MinionState
{
    public MinionState_Dead(string stateName, MinionController manager, Animator animator, MinionStateMachine stateMachine) : base(stateName, manager, animator, stateMachine) { }
    public override void Enter()
    {
        base.Enter();
        minion.isDead = true;
        minion.Dead();
    }
}

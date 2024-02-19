using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionState : EnmeyState
{
    public MinionState(string stateName, MinionController manager, Animator animator, MinionStateMachine stateMachine)
    {
        animName = stateName;
        minion = manager;
        parameters = manager.parameters;
        Initialize(animator, manager, stateMachine);
    }

    protected MinionController minion;
    protected MinionStateMachine stateMachine;
    protected MinionParameters parameters;



    public void Initialize(Animator animator, MinionController minion, MinionStateMachine stateMachine)
    {
        this.animator = animator;
        this.minion = minion;
        this.stateMachine = stateMachine;
    }
}

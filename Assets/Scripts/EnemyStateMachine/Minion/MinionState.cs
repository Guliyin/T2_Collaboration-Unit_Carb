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
        this.stateMachine = stateMachine;
        this.animator = animator;
        Initialize();
    }

    protected Vector3 currentSpeed;
    protected MinionController minion;
    protected MinionStateMachine stateMachine;
    protected MinionParameters parameters;



    public void Initialize()
    {
        stateHash = Animator.StringToHash(animName);
    }
}

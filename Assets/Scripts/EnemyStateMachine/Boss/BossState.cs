using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : EnmeyState
{
    public BossState(string stateName, BossController manager, Animator animator, BossStateMachine stateMachine)
    {
        animName = stateName;
        boss = manager;
        parameters = manager.parameters;
        this.stateMachine = stateMachine;
        this.animator = animator;
        Initialize();
    }

    protected Vector3 currentSpeed;
    protected BossController boss;
    protected BossStateMachine stateMachine;
    protected BossParameters parameters;

    public void Initialize()
    {
        stateHash = Animator.StringToHash(animName);
    }
    public override void Enter()
    {
        base.Enter();
    }
}

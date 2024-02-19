using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : EnmeyState
{
    protected BossController boss;
    protected BossStateMachine stateMachine;

    public void Initialize(Animator animator, BossController boss, BossStateMachine stateMachine)
    {
        this.animator = animator;
        this.boss = boss;
        this.stateMachine = stateMachine;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState_Chase : BossState
{
    public BossState_Chase(string stateName, BossController manager, Animator animator, BossStateMachine stateMachine) : base(stateName, manager, animator, stateMachine) { }

    float timer = 0;

    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            boss.Move(boss.player.position);
        }
    }
    public override void PhysicUpdate()
    {
        
    }
}

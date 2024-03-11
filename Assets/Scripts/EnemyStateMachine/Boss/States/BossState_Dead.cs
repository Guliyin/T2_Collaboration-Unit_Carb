using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState_Dead : BossState
{
    public BossState_Dead(string stateName, BossController manager, Animator animator, BossStateMachine stateMachine) : base(stateName, manager, animator, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        AudioManager.Instance.PlayAudio("Boss_Die");
        boss.isDead = true;
        EventCenter.Broadcast(FunctionType.PlayerWin);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionState_Attack : MinionState
{
    public MinionState_Attack(string stateName, MinionController manager, Animator animator, MinionStateMachine stateMachine) : base(stateName, manager, animator, stateMachine) { }
    public override void Enter()
    {
        //transitionDuration = 0f;
        base.Enter();
    }
    public override void LogicUpdate()
    {
        if (IsAnimationFinished)
        {
            stateMachine.SwitchState(typeof(MinionState_AfterAttack));
        }
        //Vector3 targetDir = minion.player.position - minion.transform.position;
        //currentSpeed = Vector3.MoveTowards(currentSpeed, targetDir.normalized * parameters.AttackSpeed, parameters.acceleration * Time.deltaTime);
    }
    public override void PhysicUpdate()
    {
        //minion.Move(currentSpeed);
        minion.Move(parameters.AttackSpeed * minion.transform.forward);
    }
}

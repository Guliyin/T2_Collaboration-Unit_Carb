using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionState_PrepareAttack : MinionState
{
    public MinionState_PrepareAttack(string stateName, MinionController manager, Animator animator, MinionStateMachine stateMachine) : base(stateName, manager, animator, stateMachine) { }
    public override void Enter()
    {
        base.Enter();
        minion.attackArea.SetActive(true);
    }
    public override void LogicUpdate()
    {
        Vector3 direction = minion.player.position - minion.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        minion.transform.rotation = Quaternion.Lerp(minion.transform.rotation, toRotation, Time.deltaTime * parameters.turnRate * 2);

        if (IsAnimationFinished)
        {
            stateMachine.SwitchState(typeof(MinionState_Attack));
        }
    }
    public override void Exit()
    {
        minion.attackArea.SetActive(false);
    }
}

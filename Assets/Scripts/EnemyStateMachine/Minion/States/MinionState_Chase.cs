using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionState_Chase : MinionState
{
    public MinionState_Chase(string stateName, MinionController manager, Animator animator, MinionStateMachine stateMachine) : base(stateName, manager, animator, stateMachine) { }
    
    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        Vector3 direction = minion.player.position - minion.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        minion.transform.rotation = Quaternion.Lerp(minion.transform.rotation, toRotation, Time.deltaTime * parameters.turnRate);
        minion.transform.Translate(Vector3.forward * parameters.moveSpeed * Time.deltaTime);



        // Vector3 targetDir = minion.player.position - minion.transform.position;
        //currentSpeed = Vector3.MoveTowards(currentSpeed, targetDir.normalized * runSpeed, acceleration * Time.deltaTime);
    }
}

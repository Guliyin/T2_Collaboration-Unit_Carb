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
        //minion.transform.Translate(Vector3.forward * parameters.moveSpeed * Time.deltaTime);

        if (direction.magnitude <= parameters.attackDistance)
        {
            stateMachine.SwitchState(typeof(MinionState_PrepareAttack));
        }
        currentSpeed = Vector3.MoveTowards(currentSpeed, direction.normalized * parameters.moveSpeed, parameters.acceleration * Time.deltaTime);
    }
    public override void PhysicUpdate()
    {
        minion.Move(currentSpeed);
    }
}

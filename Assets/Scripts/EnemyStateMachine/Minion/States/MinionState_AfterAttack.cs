using UnityEngine;

public class MinionState_AfterAttack : MinionState
{
    public MinionState_AfterAttack(string stateName, MinionController manager, Animator animator, MinionStateMachine stateMachine) : base(stateName, manager, animator, stateMachine) { }
    public override void Enter()
    {
        base.Enter();
        currentSpeed = minion.MoveSpeed;
    }
    public override void LogicUpdate()
    {
        if (IsAnimationFinished)
        {
            stateMachine.SwitchState(typeof(MinionState_Chase));
        }
        currentSpeed = Vector3.MoveTowards(currentSpeed, Vector3.zero, parameters.deceleration * Time.deltaTime);
    }
    public override void PhysicUpdate()
    {
        minion.Move(currentSpeed);
    }
}

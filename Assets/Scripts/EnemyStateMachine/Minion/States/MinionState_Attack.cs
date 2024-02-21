using UnityEngine;

public class MinionState_Attack : MinionState
{
    public MinionState_Attack(string stateName, MinionController manager, Animator animator, MinionStateMachine stateMachine) : base(stateName, manager, animator, stateMachine) { }
    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        if (IsAnimationFinished)
        {
            stateMachine.SwitchState(typeof(MinionState_AfterAttack));
        }
    }
    public override void PhysicUpdate()
    {
        minion.Move(parameters.attackSpeed * minion.transform.forward);
    }
}

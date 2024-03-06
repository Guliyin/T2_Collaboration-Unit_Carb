using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "States/Player/Hit", fileName = "PlayerState_Hit")]
public class PlayerState_Hit : PlayerState
{
    [SerializeField] float stuntTime;
    [SerializeField] float stuntForce;
    [SerializeField] float deceleration;
    public override void Enter()
    {
        base.Enter();
        player.SetRigWeight(0);
        Vector3 dir = player.DamagePos - player.transform.position;
        currentSpeed = stuntForce * dir.normalized * -1;
    }
    public override void LogicUpdate()
    {
        player.DeductStamina(0);
        if (IsAnimationFinished)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        currentSpeed = Vector3.MoveTowards(currentSpeed, Vector3.zero, deceleration * Time.deltaTime);
    }
    public override void PhysicUpdate()
    {
        player.Move(currentSpeed);
    }
    public override void Exit()
    {
        player.SetRigWeight(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/Dead", fileName = "PlayerState_Dead")]
public class PlayerState_Dead : PlayerState
{
    [SerializeField] float stuntForce;
    [SerializeField] float deceleration;

    public override void Enter()
    {
        base.Enter();
        player.isDead = true;
        player.SetRigWeight(0);
        EventCenter.Broadcast(FunctionType.PlayerDead);

        Vector3 dir = player.DamagePos - player.transform.position;
        currentSpeed = stuntForce * dir.normalized * -1;
    }
    public override void LogicUpdate()
    {
        currentSpeed = Vector3.MoveTowards(currentSpeed, Vector3.zero, deceleration * Time.deltaTime);
    }
    public override void PhysicUpdate()
    {
        player.Move(currentSpeed);
    }
    public override void Exit()
    {
        player.SetRigWeight(1);
        player.isDead = false;
    }
}

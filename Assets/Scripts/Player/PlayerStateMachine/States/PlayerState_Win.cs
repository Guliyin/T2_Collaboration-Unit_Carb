using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/Win", fileName = "PlayerState_Win")]
public class PlayerState_Win : PlayerState
{
    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        if (IsAnimationFinished)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
    }
}

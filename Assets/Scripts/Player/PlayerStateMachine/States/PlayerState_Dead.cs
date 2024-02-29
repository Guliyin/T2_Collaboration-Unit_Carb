using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/Dead", fileName = "PlayerState_Dead")]
public class PlayerState_Dead : PlayerState
{
    [SerializeField] float resetTimer;

    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        if (StateDuration > resetTimer)
        {
            GameManager.Instance.ReloadCurrentLevel();
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
    }
}

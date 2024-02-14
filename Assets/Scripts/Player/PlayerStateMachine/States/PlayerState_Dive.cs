using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "States/Player/Dive", fileName = "PlayerState_Dive")]
public class PlayerState_Dive : PlayerState
{
    [SerializeField] float divingTime;
    public override void Enter()
    {
        base.Enter();
        player.mesh.gameObject.SetActive(false);
        player.transform.DOMove(player.fromToPosTemp[1], divingTime);
    }
    public override void LogicUpdate()
    {
        if (StateDuration >= divingTime)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
    }
    public override void Exit()
    {
        player.mesh.gameObject.SetActive(true);
    }
}

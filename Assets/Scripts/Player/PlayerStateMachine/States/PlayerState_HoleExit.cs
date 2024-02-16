using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "States/Player/HoleExit", fileName = "PlayerState_HoleExit")]
public class PlayerState_HoleExit : PlayerState
{
    [SerializeField] float JumpSpeed;
    [SerializeField] float JumpForce;
    public override void Enter()
    {
        base.Enter();
        Vector3 speed = player.transform.forward * JumpSpeed;
        player.Move(speed);
        player.SetVerticalVelocity(JumpForce);
    }
    public override void LogicUpdate()
    {
        if (StateDuration >= 1)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
    }
}

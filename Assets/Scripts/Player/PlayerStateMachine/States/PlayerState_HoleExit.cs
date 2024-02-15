using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "States/Player/HoleExit", fileName = "PlayerState_HoleExit")]
public class PlayerState_HoleExit : PlayerState
{
    [SerializeField] float jumpTime;
    [SerializeField] float fallTime;
    [SerializeField] float Height;
    public override void Enter()
    {
        base.Enter();

        //var anim1 = player.transform.DOMove(player.fromToPosTemp[0] + new Vector3(0, Height, 0), jumpTime);
        ////var anim2 = player.transform.DOMove(player.fromToPosTemp[0] + new Vector3(0, -0.5f, 0), fallTime);

        //Sequence sequence = DOTween.Sequence();
        //sequence.Append(anim1);
        //sequence.Append(anim2);
        //sequence.Play();
        Vector3 speed = player.transform.forward * 5;
        player.Move(speed);
        player.SetVerticalVelocity(Height);
    }
    public override void LogicUpdate()
    {
        if (StateDuration >= 1)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
    }
}

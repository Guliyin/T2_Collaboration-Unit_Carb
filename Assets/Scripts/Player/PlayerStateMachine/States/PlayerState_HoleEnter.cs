using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "States/Player/HoleEnter", fileName = "PlayerState_HoleEnter")]
public class PlayerState_HoleEnter : PlayerState
{
    [SerializeField] float jumpTime;
    [SerializeField] float fallTime;
    [SerializeField] float Height;
    public override void Enter()
    {
        base.Enter();

        var anim1 = player.transform.DOMove(player.fromToPosTemp[0] + new Vector3(0, Height, 0), jumpTime);
        var anim2 = player.transform.DOMove(player.fromToPosTemp[0] + new Vector3(0, -0.5f, 0), fallTime);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(anim1);
        sequence.Append(anim2);
        sequence.Play();
    }
    public override void LogicUpdate()
    {
        if (StateDuration >= jumpTime + fallTime)
        {
            stateMachine.SwitchState(typeof(PlayerState_Dive));
        }
    }
}

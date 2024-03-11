using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "States/Player/Dive", fileName = "PlayerState_Dive")]
public class PlayerState_Dive : PlayerState
{
    [SerializeField] float divingTime;
    public override void Enter()
    {
        base.Enter();
        AudioManager.Instance.PlayAudio("Player_Teleport");
        player.isDashing = true;
        player.mesh.gameObject.SetActive(false);
        player.transform.DOMove(player.fromToPosTemp[1], divingTime);
        player.UseGravity = false;
    }
    public override void LogicUpdate()
    {
        player.DeductStamina(0);

        if (StateDuration >= divingTime)
        {
            stateMachine.SwitchState(typeof(PlayerState_HoleExit));
        }
    }
    public override void Exit()
    {
        player.mesh.gameObject.SetActive(true);
        player.UseGravity = true;
        player.isDashing = false;
    }
}

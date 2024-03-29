using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/LeftAttack", fileName = "PlayerState_LeftAttack")]
public class PlayerState_LeftAttack : PlayerState
{
    [SerializeField] float staminaCost;
    [SerializeField] float deceleration = 30;
    [SerializeField] float moveSpeed = 6;
    [SerializeField] float cancel = 0.5f;

    [SerializeField] AudioClip audio;
    public override void Enter()
    {
        base.Enter();
        //player.DeductStamina(staminaCost);
        AudioManager.Instance.PlayAudio("Left Attack_1");
        player.Move(moveSpeed * player.transform.forward);
        currentSpeed = player.MoveSpeed;
        if (player.isLocking)
        {
            player.transform.rotation = Quaternion.Euler(0, Quaternion.LookRotation(player.enemy.position - player.transform.position).eulerAngles.y, 0);
        }
    }
    public override void LogicUpdate()
    {
        if (input.LeftAttack)
        {
            input.SetAttackInputBufferTimer(1);
        }
        if (input.RightAttack)
        {
            input.SetAttackInputBufferTimer(2);
        }
        if (input.Dash && player.HasStamina)
        {
            stateMachine.SwitchState(typeof(PlayerState_Dash));
        }
        if (NormalizedAnimPlayed > cancel)
        {
            if (input.HasAttackInputBuffer == 1 && player.HasStamina)
            {
                stateMachine.SwitchState(typeof(PlayerState_FastLeftAttack2));
            }
            else if (input.HasAttackInputBuffer == 2 && player.HasStamina)
            {
                stateMachine.SwitchState(typeof(PlayerState_FastRightAttack));
            }
        }
        if (IsAnimationFinished)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }

        currentSpeed = Vector3.MoveTowards(currentSpeed, Vector3.zero, deceleration * Time.deltaTime);
        animator.SetFloat("speed", currentSpeed.magnitude);
    }
    public override void PhysicUpdate()
    {
        player.Move(currentSpeed);
    }
    public override void Exit()
    {
        player.ClearHitCache();
    }
}

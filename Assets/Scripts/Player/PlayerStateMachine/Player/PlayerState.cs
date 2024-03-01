using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : ScriptableObject, IState
{
    [SerializeField] protected string animName;
    [SerializeField, Range(0f, 1f)] protected float transitionDuration = 0.1f;
    protected int stateHash;
    protected float stateStartTime;

    protected Vector3 currentSpeed;
    protected Animator animator;
    protected PlayerController player;
    protected CustomPlayerInput input;
    protected PlayerStateMachine stateMachine;

    protected bool IsAnimationFinished => StateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;
    protected float StateDuration => Time.time - stateStartTime;
    protected float NormalizedAnimPlayed => StateDuration / animator.GetCurrentAnimatorStateInfo(0).length;

    public void Initialize(Animator animator, PlayerController player, CustomPlayerInput input, PlayerStateMachine stateMachine)
    {
        this.animator = animator;
        this.player = player;
        this.input = input;
        this.stateMachine = stateMachine;
    }
    private void OnEnable()
    {
        stateHash = Animator.StringToHash(animName);
    }

    public virtual void Enter()
    {
        animator.CrossFadeInFixedTime(stateHash, transitionDuration);
        stateStartTime = Time.time;
    }

    public virtual void Exit() { }

    public virtual void LogicUpdate() { }

    public virtual void PhysicUpdate() { }
}

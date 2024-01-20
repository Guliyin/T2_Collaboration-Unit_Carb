using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : ScriptableObject, IState
{
    [SerializeField] string animName;
    [SerializeField, Range(0f, 1f)] protected float transitionDuration = 0f;
    int stateHash;
    float stateStartTime;
    protected Vector3 currentSpeed;
    protected Animator animator;
    protected PlayerController player;
    protected PlayerInput input;
    protected PlayerStateMachine stateMachine;

    protected bool IsAnimationFinished => StateDuration >= 0;
    protected float StateDuration => Time.time - stateStartTime;

    public void Initialize(Animator animator, PlayerController player, PlayerInput input, PlayerStateMachine stateMachine)
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
        animator.CrossFade(stateHash, transitionDuration);
        stateStartTime = Time.time;
    }

    public virtual void Exit() { }

    public virtual void LogicUpdate() { }

    public virtual void PhysicUpdate() { }
}

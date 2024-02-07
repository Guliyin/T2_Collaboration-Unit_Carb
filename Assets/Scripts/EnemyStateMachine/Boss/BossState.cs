using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : ScriptableObject, IState
{
    [SerializeField] string animName;
    [SerializeField, Range(0f, 1f)] protected float transitionDuration = 0.1f;
    int stateHash;
    float stateStartTime;
    protected Animator animator;
    protected BossController boss;
    protected BossStateMachine stateMachine;

    protected bool IsAnimationFinished => StateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;
    protected float StateDuration => Time.time - stateStartTime;

    public void Initialize(Animator animator, BossController boss, BossStateMachine stateMachine)
    {
        this.animator = animator;
        this.boss = boss;
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

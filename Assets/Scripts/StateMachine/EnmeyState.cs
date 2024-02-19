using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnmeyState : IState
{
    [SerializeField] protected string animName;
    [SerializeField, Range(0f, 1f)] protected float transitionDuration = 0.1f;
    protected int stateHash;
    float stateStartTime;
    protected Animator animator;

    protected bool IsAnimationFinished => StateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;
    protected float StateDuration => Time.time - stateStartTime;

    public virtual void Enter()
    {
        animator.CrossFadeInFixedTime(stateHash, transitionDuration);
        stateStartTime = Time.time;
    }

    public virtual void Exit() { }

    public virtual void LogicUpdate() { }

    public virtual void PhysicUpdate() { }
}
